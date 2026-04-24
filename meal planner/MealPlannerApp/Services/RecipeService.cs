using MealPlannerApp.Data;
using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApp.Services;

/// <summary>
/// Manages recipes, visibility, ownership, and review state.
/// </summary>
public class RecipeService : IRecipeService
{
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// Receives the EF Core context.
    /// </summary>
    public RecipeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Gets recipes visible to the current user with filters.
    /// </summary>
    public async Task<IEnumerable<Recipe>> GetAllRecipes(
        int? currentUserId,
        bool isAdmin,
        string? ingredientName = null,
        bool vegetarianOnly = false,
        bool highProteinOnly = false)
    {
        // Starts from the visibility rules before applying search filters.
        IQueryable<Recipe> query = GetVisibleRecipesQuery(currentUserId, isAdmin)
            .Include(r => r.Owner)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient);

        if (!string.IsNullOrWhiteSpace(ingredientName))
        {
            query = query.Where(r =>
                r.RecipeIngredients.Any(ri =>
                    ri.Ingredient.Name.Contains(ingredientName)));
        }

        if (vegetarianOnly)
        {
            query = query.Where(r => r.IsVegetarian);
        }

        if (highProteinOnly)
        {
            // Uses ingredient names to find simple high-protein recipes.
            var proteinKeywords = new[] { "chicken", "beef", "turkey", "egg", "tuna", "salmon", "tofu", "lentil", "beans", "yogurt" };
            query = query.Where(r =>
                r.RecipeIngredients.Any(ri =>
                    proteinKeywords.Any(k =>
                        ri.Ingredient.Name.ToLower().Contains(k))));
        }

        return await query
            .OrderByDescending(r => r.ApprovalStatus == ApprovalStatus.Approved)
            .ThenBy(r => r.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Gets one recipe when visible to the current user.
    /// </summary>
    public async Task<Recipe?> GetRecipeById(int id, int? currentUserId, bool isAdmin)
    {
        return await GetVisibleRecipesQuery(currentUserId, isAdmin)
            .Include(r => r.Owner)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    /// <summary>
    /// Creates a recipe as draft or pending review.
    /// </summary>
    public async Task<Recipe> CreateRecipe(int ownerId, Recipe recipe, bool submitForReview)
    {
        recipe.OwnerId = ownerId;
        recipe.CreatedAt = DateTime.UtcNow;
        ApplyReviewState(recipe, submitForReview ? ApprovalStatus.PendingReview : ApprovalStatus.Draft, null);

        _dbContext.Recipes.Add(recipe);
        await _dbContext.SaveChangesAsync();
        return recipe;
    }

    /// <summary>
    /// Updates a recipe when the user can manage it.
    /// </summary>
    public async Task<bool> UpdateRecipe(int recipeId, int ownerId, bool isAdmin, Recipe recipe, bool submitForReview)
    {
        var existingRecipe = await _dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
        if (existingRecipe is null)
        {
            return false;
        }

        if (!CanManage(existingRecipe, ownerId, isAdmin))
        {
            return false;
        }

        existingRecipe.Name = recipe.Name;
        existingRecipe.Description = recipe.Description;
        existingRecipe.Instructions = recipe.Instructions;
        existingRecipe.CookingTime = recipe.CookingTime;
        existingRecipe.Calories = recipe.Calories;
        existingRecipe.IsVegetarian = recipe.IsVegetarian;

        if (isAdmin)
        {
            existingRecipe.ApprovalStatus = submitForReview ? ApprovalStatus.Approved : existingRecipe.ApprovalStatus;
        }
        else
        {
            var nextStatus = existingRecipe.ApprovalStatus == ApprovalStatus.Approved
                ? ApprovalStatus.PendingReview
                : submitForReview
                    ? ApprovalStatus.PendingReview
                    : ApprovalStatus.Draft;
            ApplyReviewState(existingRecipe, nextStatus, null);
        }

        await _dbContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Deletes a recipe when allowed and unused.
    /// </summary>
    public async Task<DeleteOperationResult> DeleteRecipe(int id, int ownerId, bool isAdmin)
    {
        var recipe = await _dbContext.Recipes.FindAsync(id);
        if (recipe is null)
        {
            return DeleteOperationResult.NotFound;
        }

        if (!CanManage(recipe, ownerId, isAdmin))
        {
            return DeleteOperationResult.Forbidden;
        }

        var isInUse = await _dbContext.Meals.AnyAsync(m => m.RecipeId == id)
            || await _dbContext.MealPlanTemplateMeals.AnyAsync(m => m.RecipeId == id);
        if (isInUse)
        {
            return DeleteOperationResult.InUse;
        }

        _dbContext.Recipes.Remove(recipe);
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return DeleteOperationResult.InUse;
        }

        return DeleteOperationResult.Deleted;
    }

    /// <summary>
    /// Sends a recipe to admin review.
    /// </summary>
    public async Task<bool> SubmitForReview(int id, int ownerId, bool isAdmin)
    {
        var recipe = await _dbContext.Recipes.FindAsync(id);
        if (recipe is null)
        {
            return false;
        }

        if (!CanManage(recipe, ownerId, isAdmin) || recipe.ApprovalStatus == ApprovalStatus.Approved)
        {
            return false;
        }

        ApplyReviewState(recipe, ApprovalStatus.PendingReview, null);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Gets all recipes waiting for review.
    /// </summary>
    public async Task<IReadOnlyCollection<Recipe>> GetPendingRecipes()
    {
        return await _dbContext.Recipes
            .Where(r => r.ApprovalStatus == ApprovalStatus.PendingReview)
            .Include(r => r.Owner)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .OrderBy(r => r.SubmittedAt)
            .ToListAsync();
    }

    /// <summary>
    /// Marks a recipe as approved.
    /// </summary>
    public async Task<bool> ApproveRecipe(int id)
    {
        var recipe = await _dbContext.Recipes.FindAsync(id);
        if (recipe is null)
        {
            return false;
        }

        ApplyReviewState(recipe, ApprovalStatus.Approved, null);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Marks a recipe as rejected with notes.
    /// </summary>
    public async Task<bool> RejectRecipe(int id, string? reviewNotes)
    {
        var recipe = await _dbContext.Recipes.FindAsync(id);
        if (recipe is null)
        {
            return false;
        }

        ApplyReviewState(recipe, ApprovalStatus.Rejected, reviewNotes);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Applies public, owner, and admin visibility rules.
    /// </summary>
    private IQueryable<Recipe> GetVisibleRecipesQuery(int? currentUserId, bool isAdmin)
    {
        var query = _dbContext.Recipes.AsQueryable();

        if (isAdmin)
        {
            return query;
        }

        if (!currentUserId.HasValue)
        {
            return query.Where(r => r.ApprovalStatus == ApprovalStatus.Approved);
        }

        return query.Where(r =>
            r.ApprovalStatus == ApprovalStatus.Approved ||
            r.OwnerId == currentUserId.Value);
    }

    /// <summary>
    /// Checks whether a user can manage the recipe.
    /// </summary>
    private static bool CanManage(Recipe recipe, int ownerId, bool isAdmin)
    {
        return isAdmin || recipe.OwnerId == ownerId;
    }

    /// <summary>
    /// Updates review status and review timestamps.
    /// </summary>
    private static void ApplyReviewState(Recipe recipe, ApprovalStatus status, string? reviewNotes)
    {
        recipe.ApprovalStatus = status;
        recipe.ReviewNotes = string.IsNullOrWhiteSpace(reviewNotes) ? null : reviewNotes.Trim();

        if (status == ApprovalStatus.PendingReview)
        {
            recipe.SubmittedAt = DateTime.UtcNow;
            recipe.ReviewedAt = null;
            recipe.ReviewNotes = null;
            return;
        }

        if (status is ApprovalStatus.Approved or ApprovalStatus.Rejected)
        {
            recipe.ReviewedAt = DateTime.UtcNow;
            return;
        }

        recipe.SubmittedAt = null;
        recipe.ReviewedAt = null;
    }
}
