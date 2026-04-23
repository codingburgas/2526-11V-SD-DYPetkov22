namespace MealPlannerApp.Dtos.MealPlans;

/// <summary>
/// Progress page values.
/// </summary>
public class MealPlanHistoryDto
{
    /// <summary>Recent weekly summaries.</summary>
    public IReadOnlyCollection<WeeklyProgressSummaryDto> Weeks { get; set; } = Array.Empty<WeeklyProgressSummaryDto>();

    /// <summary>Average daily nutrition across weeks.</summary>
    public NutritionSummaryDto AverageDailyNutrition { get; set; } = new();

    /// <summary>Average daily calories across weeks.</summary>
    public int AverageDailyCalories { get; set; }
}

/// <summary>
/// One week of progress totals.
/// </summary>
public class WeeklyProgressSummaryDto
{
    /// <summary>Monday date for the week.</summary>
    public DateTime WeekStart { get; set; }

    /// <summary>Sunday date for the week.</summary>
    public DateTime WeekEnd { get; set; }

    /// <summary>Total meals planned.</summary>
    public int MealsCount { get; set; }

    /// <summary>Days that contain meals.</summary>
    public int DaysWithMeals { get; set; }

    /// <summary>Total weekly calories.</summary>
    public int TotalCalories { get; set; }

    /// <summary>Average daily calories.</summary>
    public int AverageDailyCalories { get; set; }

    /// <summary>Total weekly nutrition.</summary>
    public NutritionSummaryDto TotalNutrition { get; set; } = new();

    /// <summary>Average daily nutrition.</summary>
    public NutritionSummaryDto AverageDailyNutrition { get; set; } = new();
}
