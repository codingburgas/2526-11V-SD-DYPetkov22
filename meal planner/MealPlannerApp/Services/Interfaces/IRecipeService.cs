using MealPlannerApp.Models;
using MealPlannerApp.Services.Models;

namespace MealPlannerApp.Services.Interfaces;

/// <summary>
/// Defines recipe catalog and moderation operations.
/// </summary>
public interface IRecipeService
{
    /// <summary>Gets visible recipes with filters.</summary>
    Task<IEnumerable<Recipe>> GetAllRecipes(int? currentUserId, bool isAdmin, string? ingredientName = null, bool vegetarianOnly = false, bool highProteinOnly = false);

    /// <summary>Gets one visible recipe.</summary>
    Task<Recipe?> GetRecipeById(int id, int? currentUserId, bool isAdmin);

    /// <summary>Creates a recipe.</summary>
    Task<Recipe> CreateRecipe(int ownerId, Recipe recipe, bool submitForReview);

    /// <summary>Updates a recipe.</summary>
    Task<bool> UpdateRecipe(int recipeId, int ownerId, bool isAdmin, Recipe recipe, bool submitForReview);

    /// <summary>Deletes a recipe when possible.</summary>
    Task<DeleteOperationResult> DeleteRecipe(int id, int ownerId, bool isAdmin);

    /// <summary>Submits a recipe for review.</summary>
    Task<bool> SubmitForReview(int id, int ownerId, bool isAdmin);

    /// <summary>Gets recipes waiting for review.</summary>
    Task<IReadOnlyCollection<Recipe>> GetPendingRecipes();

    /// <summary>Approves a recipe.</summary>
    Task<bool> ApproveRecipe(int id);

    /// <summary>Rejects a recipe.</summary>
    Task<bool> RejectRecipe(int id, string? reviewNotes);
}
