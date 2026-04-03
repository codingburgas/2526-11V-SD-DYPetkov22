using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Dtos.MealPlanTemplates;

public class CreateMealPlanTemplateDto
{
    [Required]
    [DataType(DataType.Date)]
    public DateTime WeekStart { get; set; }

    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public bool SubmitForReview { get; set; } = true;
}
