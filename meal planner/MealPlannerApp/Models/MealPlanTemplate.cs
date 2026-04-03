using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Models;

public class MealPlanTemplate : BaseEntity
{
    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime WeekStart { get; set; }

    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!;

    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Draft;

    [StringLength(500)]
    public string? ReviewNotes { get; set; }

    public DateTime? SubmittedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }

    public ICollection<MealPlanTemplateMeal> Meals { get; set; } = new List<MealPlanTemplateMeal>();
}
