using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Dtos.Moderation;

/// <summary>
/// Admin approve/reject form values.
/// </summary>
public class ReviewDecisionDto
{
    /// <summary>Entity id being reviewed.</summary>
    public int Id { get; set; }

    /// <summary>Feedback shown to the owner.</summary>
    [StringLength(500)]
    public string? ReviewNotes { get; set; }
}
