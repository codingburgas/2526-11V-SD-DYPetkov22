namespace MealPlannerApp.Dtos.MealPlans;

/// <summary>
/// Main dashboard values for a selected week.
/// </summary>
public class WeeklyMealPlannerDto
{
    /// <summary>Monday date for the week.</summary>
    public DateTime WeekStart { get; set; }

    /// <summary>Sunday date for the week.</summary>
    public DateTime WeekEnd { get; set; }

    /// <summary>Day currently focused in the dashboard.</summary>
    public DateTime SelectedDate { get; set; }

    /// <summary>Total weekly calories.</summary>
    public int WeeklyTotalCalories { get; set; }

    /// <summary>Total weekly nutrition.</summary>
    public NutritionSummaryDto WeeklyNutrition { get; set; } = new();

    /// <summary>Target nutrition for the full week.</summary>
    public NutritionSummaryDto WeeklyNutritionTarget { get; set; } = new();

    /// <summary>Target nutrition for one day.</summary>
    public NutritionSummaryDto DailyNutritionTarget { get; set; } = new();

    /// <summary>Seven dashboard days.</summary>
    public IReadOnlyCollection<WeeklyDayDto> Days { get; set; } = Array.Empty<WeeklyDayDto>();

    /// <summary>Top ingredients for the week.</summary>
    public IReadOnlyCollection<MostUsedIngredientDto> MostUsedIngredients { get; set; } = Array.Empty<MostUsedIngredientDto>();

    /// <summary>Ingredient choices for preference inputs.</summary>
    public IReadOnlyCollection<PlannerIngredientOptionDto> AvailableIngredients { get; set; } = Array.Empty<PlannerIngredientOptionDto>();

    /// <summary>Preset plan choices.</summary>
    public IReadOnlyCollection<PresetMealPlanOptionDto> PresetPlans { get; set; } = Array.Empty<PresetMealPlanOptionDto>();

    /// <summary>Personalized generator form model.</summary>
    public GeneratePersonalizedMealPlanDto GeneratePlan { get; set; } = new();

    /// <summary>Preset starter form model.</summary>
    public StartPresetMealPlanDto StartPresetPlan { get; set; } = new();
}

/// <summary>
/// One day shown in the weekly dashboard.
/// </summary>
public class WeeklyDayDto
{
    /// <summary>Calendar date.</summary>
    public DateTime Date { get; set; }

    /// <summary>Total calories for the day.</summary>
    public int TotalCalories { get; set; }

    /// <summary>Total nutrition for the day.</summary>
    public NutritionSummaryDto Nutrition { get; set; } = new();

    /// <summary>Meals planned for the day.</summary>
    public IReadOnlyCollection<WeeklyMealItemDto> Meals { get; set; } = Array.Empty<WeeklyMealItemDto>();
}

/// <summary>
/// One meal card shown in the dashboard.
/// </summary>
public class WeeklyMealItemDto
{
    /// <summary>Meal id.</summary>
    public int MealId { get; set; }

    /// <summary>Recipe id.</summary>
    public int RecipeId { get; set; }

    /// <summary>Meal slot text.</summary>
    public string MealType { get; set; } = string.Empty;

    /// <summary>Recipe display name.</summary>
    public string RecipeName { get; set; } = string.Empty;

    /// <summary>Calories for this portion.</summary>
    public int Calories { get; set; }

    /// <summary>Recipe portion scaling.</summary>
    public double PortionMultiplier { get; set; }

    /// <summary>Nutrition for this portion.</summary>
    public NutritionSummaryDto Nutrition { get; set; } = new();
}

/// <summary>
/// Ingredient usage summary for the week.
/// </summary>
public class MostUsedIngredientDto
{
    /// <summary>Ingredient name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Number of uses.</summary>
    public int UsageCount { get; set; }

    /// <summary>Total grams used.</summary>
    public int TotalQuantityInGrams { get; set; }
}

/// <summary>
/// Ingredient option for planner forms.
/// </summary>
public class PlannerIngredientOptionDto
{
    /// <summary>Ingredient id.</summary>
    public int Id { get; set; }

    /// <summary>Ingredient name.</summary>
    public string Name { get; set; } = string.Empty;
}
