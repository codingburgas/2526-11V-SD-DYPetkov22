using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Models;

/// <summary>
/// One meal stored inside a weekly template.
/// </summary>
public class MealPlanTemplateMeal : BaseEntity
{
    /// <summary>Parent template id.</summary>
    public int MealPlanTemplateId { get; set; }

    /// <summary>Parent template.</summary>
    public MealPlanTemplate MealPlanTemplate { get; set; } = null!;

    [Range(0, 6)]
    /// <summary>Days after template week start.</summary>
    public int DayOffset { get; set; }

    /// <summary>Breakfast, lunch, or dinner.</summary>
    public MealType MealType { get; set; }

    /// <summary>Stored recipe id.</summary>
    public int RecipeId { get; set; }

    /// <summary>Stored recipe.</summary>
    public Recipe Recipe { get; set; } = null!;

    [Range(0.5, 3.0)]
    /// <summary>Recipe portion scaling.</summary>
    public double PortionMultiplier { get; set; } = 1.0;
}
