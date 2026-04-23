using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Dtos.MealPlans;

/// <summary>
/// Meal plan day values shown on pages.
/// </summary>
public class MealPlanDto
{
    /// <summary>Meal plan day id.</summary>
    public int Id { get; set; }

    /// <summary>Calendar date for the day.</summary>
    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    /// <summary>Total meals planned for the day.</summary>
    public int MealsCount { get; set; }

    /// <summary>Total calories planned for the day.</summary>
    public int TotalCalories { get; set; }

    /// <summary>Total nutrition planned for the day.</summary>
    public NutritionSummaryDto TotalNutrition { get; set; } = new();

    /// <summary>Meals planned for the day.</summary>
    public IReadOnlyCollection<MealPlanMealDto> Meals { get; set; } = Array.Empty<MealPlanMealDto>();

    /// <summary>Most used ingredients for the day.</summary>
    public IReadOnlyCollection<MostUsedIngredientSummaryDto> MostUsedIngredients { get; set; } = Array.Empty<MostUsedIngredientSummaryDto>();
}

/// <summary>
/// One meal shown inside a meal plan day.
/// </summary>
public class MealPlanMealDto
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
/// Ingredient usage summary for a day.
/// </summary>
public class MostUsedIngredientSummaryDto
{
    /// <summary>Ingredient name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Number of uses.</summary>
    public int UsageCount { get; set; }

    /// <summary>Total grams used.</summary>
    public int TotalQuantityInGrams { get; set; }
}
