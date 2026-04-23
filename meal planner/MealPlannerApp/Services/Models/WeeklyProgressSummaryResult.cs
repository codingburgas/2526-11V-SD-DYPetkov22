namespace MealPlannerApp.Services.Models;

/// <summary>
/// Service result for one history week.
/// </summary>
public class WeeklyProgressSummaryResult
{
    /// <summary>Monday date for the week.</summary>
    public DateTime WeekStart { get; set; }

    /// <summary>Sunday date for the week.</summary>
    public DateTime WeekEnd { get; set; }

    /// <summary>Total meals planned.</summary>
    public int MealsCount { get; set; }

    /// <summary>Days that contain meals.</summary>
    public int DaysWithMeals { get; set; }

    /// <summary>Total weekly nutrition.</summary>
    public NutritionSummaryResult TotalNutrition { get; set; } = new();
}
