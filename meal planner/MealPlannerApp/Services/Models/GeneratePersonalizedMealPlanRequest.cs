namespace MealPlannerApp.Services.Models;

/// <summary>
/// Request values for generating a custom week.
/// </summary>
public class GeneratePersonalizedMealPlanRequest
{
    /// <summary>User receiving the generated plan.</summary>
    public int UserId { get; set; }

    /// <summary>Monday date for the target week.</summary>
    public DateTime WeekStart { get; set; }

    /// <summary>Number of meals per day.</summary>
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
    public IReadOnlyCollection<string> ExcludedFoods { get; set; } = Array.Empty<string>();

    /// <summary>Ingredient ids to avoid.</summary>
    public IReadOnlyCollection<int> ExcludedIngredientIds { get; set; } = Array.Empty<int>();

    /// <summary>Ingredient ids treated as allergies.</summary>
    public IReadOnlyCollection<int> AllergyIngredientIds { get; set; } = Array.Empty<int>();
}
