using MealPlannerApp.Dtos.Moderation;
using MealPlannerApp.Dtos.Recipes;
using MealPlannerApp.Infrastructure;
using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApp.Controllers;

/// <summary>
/// Handles recipe browsing, ownership, and moderation.
/// </summary>
public class RecipesController : Controller
{
    private readonly IRecipeService _recipeService;

    /// <summary>
    /// Receives the recipe service.
    /// </summary>
    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    /// <summary>
    /// Lists visible recipes with optional filters.
    /// </summary>
    public async Task<IActionResult> Index(string? ingredientName, bool vegetarianOnly = false, bool highProteinOnly = false)
    {
        var recipes = await _recipeService.GetAllRecipes(
            GetCurrentUserId(),
            IsAdmin(),
            ingredientName,
            vegetarianOnly,
            highProteinOnly);

        var dto = recipes.Select(MapToDto).ToList();
        ViewData["IngredientName"] = ingredientName;
        ViewData["VegetarianOnly"] = vegetarianOnly;
        ViewData["HighProteinOnly"] = highProteinOnly;
        return View(dto);
    }

    /// <summary>
    /// Shows one visible recipe.
    /// </summary>
    public async Task<IActionResult> Details(int id)
    {
        var recipe = await _recipeService.GetRecipeById(id, GetCurrentUserId(), IsAdmin());
        if (recipe is null)
        {
            return NotFound();
        }

        return View(MapToDto(recipe));
    }

    /// <summary>
    /// Shows the recipe create form.
    /// </summary>
    [Authorize]
    public IActionResult Create()
    {
        return View(new RecipeDto());
    }

    /// <summary>
    /// Creates a private or submitted recipe.
    /// </summary>
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RecipeDto dto, bool submitForReview = false)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await _recipeService.CreateRecipe(User.GetRequiredUserId(), MapToEntity(dto), submitForReview);
        TempData["SuccessMessage"] = submitForReview
            ? "Recipe submitted for admin review."
            : "Recipe saved as a private draft.";
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Shows the recipe edit form.
    /// </summary>
    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var recipe = await _recipeService.GetRecipeById(id, GetCurrentUserId(), IsAdmin());
        if (recipe is null)
        {
            return NotFound();
        }

        if (!CanManage(recipe))
        {
            return Forbid();
        }

        return View(MapToDto(recipe));
    }

    /// <summary>
    /// Saves recipe edits.
    /// </summary>
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RecipeDto dto, bool submitForReview = false)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var existingRecipe = await _recipeService.GetRecipeById(id, GetCurrentUserId(), IsAdmin());
        if (existingRecipe is null)
        {
            return NotFound();
        }

        var updated = await _recipeService.UpdateRecipe(id, User.GetRequiredUserId(), IsAdmin(), MapToEntity(dto), submitForReview);
        if (!updated)
        {
            return Forbid();
        }

        TempData["SuccessMessage"] = !IsAdmin() && existingRecipe.ApprovalStatus == ApprovalStatus.Approved
            ? "Recipe updated and sent back to admin review before it becomes public again."
            : submitForReview
                ? "Recipe updated and submitted for admin review."
                : "Recipe changes saved.";
        return RedirectToAction(nameof(Details), new { id });
    }

    /// <summary>
    /// Shows the recipe delete confirmation.
    /// </summary>
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var recipe = await _recipeService.GetRecipeById(id, GetCurrentUserId(), IsAdmin());
        if (recipe is null)
        {
            return NotFound();
        }

        if (!CanManage(recipe))
        {
            return Forbid();
        }

        return View(MapToDto(recipe));
    }

    /// <summary>
    /// Deletes a recipe when allowed and unused.
    /// </summary>
    [Authorize]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _recipeService.DeleteRecipe(id, User.GetRequiredUserId(), IsAdmin());
        if (deleted == DeleteOperationResult.NotFound)
        {
            return NotFound();
        }

        if (deleted == DeleteOperationResult.Forbidden)
        {
            return Forbid();
        }

        if (deleted == DeleteOperationResult.InUse)
        {
            TempData["ErrorMessage"] = "This recipe is already used in meal plans or shared templates and cannot be deleted.";
            return RedirectToAction(nameof(Details), new { id });
        }

        TempData["SuccessMessage"] = "Recipe removed.";
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Sends a recipe to admin review.
    /// </summary>
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitForReview(int id)
    {
        var submitted = await _recipeService.SubmitForReview(id, User.GetRequiredUserId(), IsAdmin());
        if (!submitted)
        {
            return Forbid();
        }

        TempData["SuccessMessage"] = "Recipe submitted for admin review.";
        return RedirectToAction(nameof(Details), new { id });
    }

    /// <summary>
    /// Lists recipes waiting for admin review.
    /// </summary>
    [Authorize(Roles = ApplicationRoles.Admin)]
    public async Task<IActionResult> Moderation()
    {
        var recipes = await _recipeService.GetPendingRecipes();
        return View(recipes.Select(MapToDto).ToList());
    }

    /// <summary>
    /// Approves a submitted recipe.
    /// </summary>
    [Authorize(Roles = ApplicationRoles.Admin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id)
    {
        var approved = await _recipeService.ApproveRecipe(id);
        if (!approved)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Recipe approved and now visible to everyone.";
        return RedirectToAction(nameof(Moderation));
    }

    /// <summary>
    /// Rejects a submitted recipe with notes.
    /// </summary>
    [Authorize(Roles = ApplicationRoles.Admin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(ReviewDecisionDto dto)
    {
        var rejected = await _recipeService.RejectRecipe(dto.Id, dto.ReviewNotes);
        if (!rejected)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Recipe rejected with feedback for the owner.";
        return RedirectToAction(nameof(Moderation));
    }

    /// <summary>
    /// Reads the current user id when signed in.
    /// </summary>
    private int? GetCurrentUserId()
    {
        return User.Identity?.IsAuthenticated == true
            ? User.GetRequiredUserId()
            : null;
    }

    /// <summary>
    /// Checks whether the current user is an admin.
    /// </summary>
    private bool IsAdmin()
    {
        return User.IsInRole(ApplicationRoles.Admin);
    }

    /// <summary>
    /// Checks edit/delete permission for a recipe.
    /// </summary>
    private bool CanManage(Recipe recipe)
    {
        return IsAdmin() || recipe.OwnerId == GetCurrentUserId();
    }

    /// <summary>
    /// Converts a recipe entity to a page DTO.
    /// </summary>
    private static RecipeDto MapToDto(Recipe recipe)
    {
        return new RecipeDto
        {
            Id = recipe.Id,
            OwnerId = recipe.OwnerId,
            OwnerUserName = recipe.Owner.UserName ?? string.Empty,
            ApprovalStatus = recipe.ApprovalStatus,
            ReviewNotes = recipe.ReviewNotes,
            Name = recipe.Name,
            Description = recipe.Description,
            Instructions = recipe.Instructions,
            CookingTime = recipe.CookingTime,
            Calories = recipe.Calories,
            IsVegetarian = recipe.IsVegetarian,
            Ingredients = recipe.RecipeIngredients
                .OrderBy(ri => ri.Ingredient.Name)
                .Select(ri => new RecipeIngredientDto
                {
                    Name = ri.Ingredient.Name,
                    QuantityInGrams = ri.QuantityInGrams
                })
                .ToList()
        };
    }

    /// <summary>
    /// Converts a recipe DTO to an entity.
    /// </summary>
    private static Recipe MapToEntity(RecipeDto dto)
    {
        return new Recipe
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Instructions = dto.Instructions,
            CookingTime = dto.CookingTime,
            Calories = dto.Calories,
            IsVegetarian = dto.IsVegetarian
        };
    }
}
