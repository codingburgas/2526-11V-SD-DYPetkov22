using MealPlannerApp.Models;
using MealPlannerApp.Services.Models;

namespace MealPlannerApp.Services.Interfaces;

public interface IIngredientService
{
    Task<IEnumerable<Ingredient>> GetAllIngredients();
    Task<Ingredient?> GetIngredientById(int id);
    Task<Ingredient> CreateIngredient(Ingredient ingredient);
    Task<bool> UpdateIngredient(Ingredient ingredient);
    Task<DeleteOperationResult> DeleteIngredient(int id);
}
