using MealPlannerApp.Models;
using MealPlannerApp.Services.Models;

namespace MealPlannerApp.Services.Interfaces;

/// <summary>
/// Defines private meal plan operations.
/// </summary>
public interface IMealPlanService
{
    /// <summary>Gets all meal plan days for a user.</summary>
    Task<IEnumerable<MealPlan>> GetAllMealPlans(int userId);

    /// <summary>Gets one meal plan day.</summary>
    Task<MealPlan?> GetMealPlanById(int id, int userId);

    /// <summary>Creates one meal plan day.</summary>
    Task<MealPlan> CreateMealPlan(int userId, MealPlan mealPlan);

    /// <summary>Updates one meal plan day.</summary>
    Task<bool> UpdateMealPlan(int userId, MealPlan mealPlan);

    /// <summary>Deletes one meal plan day.</summary>
    Task<bool> DeleteMealPlan(int id, int userId);

    /// <summary>Adds a meal to a plan day.</summary>
    Task<Meal?> AddMealToPlan(int mealPlanId, int userId, bool isAdmin, Meal meal);

    /// <summary>Builds weekly plan totals.</summary>
    Task<WeeklyMealPlanResult> GetWeeklyPlan(int userId, DateTime? weekStart = null);

    /// <summary>Gets available starter plans.</summary>
    Task<IReadOnlyCollection<PresetMealPlanOptionResult>> GetPresetMealPlans();

    /// <summary>Starts a preset weekly plan.</summary>
    Task StartPresetMealPlan(StartPresetMealPlanRequest request);

    /// <summary>Generates a custom weekly plan.</summary>
    Task GeneratePersonalizedMealPlan(GeneratePersonalizedMealPlanRequest request);

    /// <summary>Gets saved planner preferences.</summary>
    Task<PlannerPreferencesResult> GetPlannerPreferences(int userId);

    /// <summary>Saves planner preferences.</summary>
    Task SavePlannerPreferences(SavePlannerPreferencesRequest request);

    /// <summary>Swaps one planned meal.</summary>
    Task<bool> SwapMeal(int mealId, int userId);

    /// <summary>Gets recent weekly progress.</summary>
    Task<IReadOnlyCollection<WeeklyProgressSummaryResult>> GetWeeklyHistory(int userId, int weeks = 8);
}
