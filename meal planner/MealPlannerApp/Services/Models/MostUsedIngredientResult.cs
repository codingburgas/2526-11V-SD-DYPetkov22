namespace MealPlannerApp.Services.Models;

/// <summary>
/// Ingredient usage total.
/// </summary>
public class MostUsedIngredientResult
{
    /// <summary>Ingredient name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Number of uses.</summary>
    public int UsageCount { get; set; }

    /// <summary>Total grams used.</summary>
    public int TotalQuantityInGrams { get; set; }
}
