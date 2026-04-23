using System.ComponentModel.DataAnnotations;
using MealPlannerApp.Models;

namespace MealPlannerApp.Dtos.MealPlans;

/// <summary>
/// Form values for adding one meal to a day.
/// </summary>
public class AddMealDto
{
    /// <summary>Target meal plan day id.</summary>
    [Range(1, int.MaxValue)]
    public int MealPlanId { get; set; }

    /// <summary>Recipe to add.</summary>
    [Range(1, int.MaxValue)]
    public int RecipeId { get; set; }

    /// <summary>Meal slot for the recipe.</summary>
    [Required]
    public MealType MealType { get; set; } = MealType.Breakfast;

    /// <summary>Week being edited.</summary>
    [DataType(DataType.Date)]
    public DateTime WeekStart { get; set; }

    /// <summary>Recipe portion scaling.</summary>
    [Display(Name = "Portion Size")]
    [Range(0.5, 3.0)]
    public double PortionMultiplier { get; set; } = 1.0;
}
