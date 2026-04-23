namespace MealPlannerApp.Services.Models;

/// <summary>
/// Calories and macro grams.
/// </summary>
public class NutritionSummaryResult
{
    /// <summary>Total calories.</summary>
    public int Calories { get; set; }

    /// <summary>Protein grams.</summary>
    public double ProteinGrams { get; set; }

    /// <summary>Carbs grams.</summary>
    public double CarbsGrams { get; set; }

    /// <summary>Fat grams.</summary>
    public double FatGrams { get; set; }
}
