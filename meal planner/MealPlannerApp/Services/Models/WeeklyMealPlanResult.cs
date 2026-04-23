using MealPlannerApp.Models;

namespace MealPlannerApp.Services.Models;

/// <summary>
/// Service result for one planner week.
/// </summary>
public class WeeklyMealPlanResult
{
    /// <summary>Monday date for the week.</summary>
    public DateTime WeekStart { get; set; }

    /// <summary>Sunday date for the week.</summary>
    public DateTime WeekEnd { get; set; }

    /// <summary>Total weekly calories.</summary>
    public int WeeklyTotalCalories { get; set; }

    /// <summary>Daily calorie totals.</summary>
    public IReadOnlyCollection<DailyCaloriesResult> DailyCalories { get; set; } = Array.Empty<DailyCaloriesResult>();

    /// <summary>Daily nutrition totals.</summary>
    public IReadOnlyCollection<DailyNutritionResult> DailyNutrition { get; set; } = Array.Empty<DailyNutritionResult>();

    /// <summary>Total weekly nutrition.</summary>
    public NutritionSummaryResult WeeklyNutrition { get; set; } = new();

    /// <summary>Most used ingredients in the week.</summary>
    public IReadOnlyCollection<MostUsedIngredientResult> MostUsedIngredients { get; set; } = Array.Empty<MostUsedIngredientResult>();

    /// <summary>Meal plan days in the week.</summary>
    public IReadOnlyCollection<MealPlan> MealPlans { get; set; } = Array.Empty<MealPlan>();
}
