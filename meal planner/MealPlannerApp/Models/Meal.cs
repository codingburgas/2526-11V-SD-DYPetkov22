using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Models;

/// <summary>
/// One recipe scheduled into a meal plan day.
/// </summary>
public class Meal : BaseEntity
{
    /// <summary>Parent meal plan day id.</summary>
    public int MealPlanId { get; set; }

    /// <summary>Parent meal plan day.</summary>
    public MealPlan MealPlan { get; set; } = null!;

    /// <summary>Scheduled recipe id.</summary>
    public int RecipeId { get; set; }

    /// <summary>Scheduled recipe.</summary>
    public Recipe Recipe { get; set; } = null!;

    /// <summary>Breakfast, lunch, or dinner.</summary>
    public MealType MealType { get; set; }

    [Range(0.5, 3.0)]
    /// <summary>Recipe portion scaling.</summary>
    public double PortionMultiplier { get; set; } = 1.0;
}
