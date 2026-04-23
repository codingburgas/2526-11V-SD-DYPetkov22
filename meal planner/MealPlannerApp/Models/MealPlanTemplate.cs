using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Models;

/// <summary>
/// Shareable copy of a full weekly meal plan.
/// </summary>
public class MealPlanTemplate : BaseEntity
{
    [Required]
    [StringLength(80)]
    /// <summary>Template name.</summary>
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    /// <summary>Optional template description.</summary>
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    /// <summary>Monday date used when this template was created.</summary>
    public DateTime WeekStart { get; set; }

    /// <summary>Owner user id.</summary>
    public int OwnerId { get; set; }

    /// <summary>Owner user.</summary>
    public User Owner { get; set; } = null!;

    /// <summary>Current review state.</summary>
    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Draft;

    [StringLength(500)]
    /// <summary>Admin feedback when rejected.</summary>
    public string? ReviewNotes { get; set; }

    /// <summary>UTC time when submitted.</summary>
    public DateTime? SubmittedAt { get; set; }

    /// <summary>UTC time when reviewed.</summary>
    public DateTime? ReviewedAt { get; set; }

    /// <summary>Meals stored in the weekly template.</summary>
    public ICollection<MealPlanTemplateMeal> Meals { get; set; } = new List<MealPlanTemplateMeal>();
}
