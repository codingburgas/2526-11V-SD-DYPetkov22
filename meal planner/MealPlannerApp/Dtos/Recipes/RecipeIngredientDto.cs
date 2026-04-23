namespace MealPlannerApp.Dtos.Recipes;

/// <summary>
/// Ingredient row shown on recipe pages.
/// </summary>
public class RecipeIngredientDto
{
    /// <summary>Ingredient name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Ingredient quantity in grams.</summary>
    public int QuantityInGrams { get; set; }
}
