using MealPlannerApp.Models;
using MealPlannerApp.Services.Models;

namespace MealPlannerApp.Services.Interfaces;

public interface IRecipeService
{
    Task<IEnumerable<Recipe>> GetAllRecipes(int? currentUserId, bool isAdmin, string? ingredientName = null, bool vegetarianOnly = false, bool highProteinOnly = false);
    Task<Recipe?> GetRecipeById(int id, int? currentUserId, bool isAdmin);
    Task<Recipe> CreateRecipe(int ownerId, Recipe recipe, bool submitForReview);
    Task<bool> UpdateRecipe(int recipeId, int ownerId, bool isAdmin, Recipe recipe, bool submitForReview);
    Task<DeleteOperationResult> DeleteRecipe(int id, int ownerId, bool isAdmin);
    Task<bool> SubmitForReview(int id, int ownerId, bool isAdmin);
    Task<IReadOnlyCollection<Recipe>> GetPendingRecipes();
    Task<bool> ApproveRecipe(int id);
    Task<bool> RejectRecipe(int id, string? reviewNotes);
}
