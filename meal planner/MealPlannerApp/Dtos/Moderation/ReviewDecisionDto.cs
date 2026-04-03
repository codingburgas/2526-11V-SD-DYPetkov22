using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Dtos.Moderation;

public class ReviewDecisionDto
{
    public int Id { get; set; }

    [StringLength(500)]
    public string? ReviewNotes { get; set; }
}
