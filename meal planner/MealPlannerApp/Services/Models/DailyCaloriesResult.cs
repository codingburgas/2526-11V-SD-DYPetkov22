namespace MealPlannerApp.Services.Models;

/// <summary>
/// Calories for one day.
/// </summary>
public class DailyCaloriesResult
{
    /// <summary>Calendar date.</summary>
    public DateTime Date { get; set; }

    /// <summary>Total calories for the date.</summary>
    public int TotalCalories { get; set; }
}
