namespace MealPlannerApp.Services.Models;

/// <summary>
/// Nutrition total for one day.
/// </summary>
public class DailyNutritionResult
{
    /// <summary>Calendar date.</summary>
    public DateTime Date { get; set; }

    /// <summary>Nutrition total for the date.</summary>
    public NutritionSummaryResult Nutrition { get; set; } = new();
}
