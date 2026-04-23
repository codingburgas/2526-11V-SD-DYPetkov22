using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Dtos.MealPlanTemplates;

/// <summary>
/// Form values for creating a template from a week.
/// </summary>
public class CreateMealPlanTemplateDto
{
    /// <summary>Monday date of the source week.</summary>
    [Required]
    [DataType(DataType.Date)]
    public DateTime WeekStart { get; set; }

    /// <summary>Template name.</summary>
    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    /// <summary>Optional template description.</summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>True when the template should go to review.</summary>
    public bool SubmitForReview { get; set; } = true;
}
