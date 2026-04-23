namespace MealPlannerApp.Services.Models;

/// <summary>
/// Saved planner preferences returned to the UI.
/// </summary>
public class PlannerPreferencesResult
{
    /// <summary>Preferred meals per day.</summary>
    public int MealsPerDay { get; set; }

    /// <summary>Body weight in kilograms.</summary>
    public double BodyWeightKg { get; set; }

    /// <summary>Daily protein target.</summary>
    public double ProteinTargetGrams { get; set; }

    /// <summary>Daily carbs target.</summary>
    public double CarbsTargetGrams { get; set; }

    /// <summary>Daily fat target.</summary>
    public double FatTargetGrams { get; set; }

    /// <summary>Free-text foods to avoid.</summary>
    public string? ExcludedFoods { get; set; }

    /// <summary>Ingredient ids to avoid.</summary>
    public IReadOnlyCollection<int> ExcludedIngredientIds { get; set; } = Array.Empty<int>();

    /// <summary>Ingredient ids treated as allergies.</summary>
    public IReadOnlyCollection<int> AllergyIngredientIds { get; set; } = Array.Empty<int>();
}
