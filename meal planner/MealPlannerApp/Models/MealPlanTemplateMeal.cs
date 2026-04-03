using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Models;

public class MealPlanTemplateMeal : BaseEntity
{
    public int MealPlanTemplateId { get; set; }
    public MealPlanTemplate MealPlanTemplate { get; set; } = null!;

    [Range(0, 6)]
    public int DayOffset { get; set; }

    public MealType MealType { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;

    [Range(0.5, 3.0)]
    public double PortionMultiplier { get; set; } = 1.0;
}
