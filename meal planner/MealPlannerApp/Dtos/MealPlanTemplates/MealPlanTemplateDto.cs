using MealPlannerApp.Models;

namespace MealPlannerApp.Dtos.MealPlanTemplates;

/// <summary>
/// Template details shown in views.
/// </summary>
public class MealPlanTemplateDto
{
    /// <summary>Template id.</summary>
    public int Id { get; set; }

    /// <summary>Monday date for the stored week.</summary>
    public DateTime WeekStart { get; set; }

    /// <summary>Template name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Template description.</summary>
    public string? Description { get; set; }

    /// <summary>Owner user id.</summary>
    public int OwnerId { get; set; }

    /// <summary>Owner username.</summary>
    public string OwnerUserName { get; set; } = string.Empty;

    /// <summary>Current review state.</summary>
    public ApprovalStatus ApprovalStatus { get; set; }

    /// <summary>Admin review notes.</summary>
    public string? ReviewNotes { get; set; }

    /// <summary>UTC creation time.</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>UTC submit time.</summary>
    public DateTime? SubmittedAt { get; set; }

    /// <summary>UTC review time.</summary>
    public DateTime? ReviewedAt { get; set; }

    /// <summary>Calories across the template week.</summary>
    public int WeeklyTotalCalories { get; set; }

    /// <summary>Seven days stored in the template.</summary>
    public IReadOnlyCollection<MealPlanTemplateDayDto> Days { get; set; } = Array.Empty<MealPlanTemplateDayDto>();
}

/// <summary>
/// One day inside a template.
/// </summary>
public class MealPlanTemplateDayDto
{
    /// <summary>Display date for the day.</summary>
    public DateTime Date { get; set; }

    /// <summary>Total calories for the day.</summary>
    public int TotalCalories { get; set; }

    /// <summary>Meals in the template day.</summary>
    public IReadOnlyCollection<MealPlanTemplateMealDto> Meals { get; set; } = Array.Empty<MealPlanTemplateMealDto>();
}

/// <summary>
/// One template meal item.
/// </summary>
public class MealPlanTemplateMealDto
{
    /// <summary>Recipe id.</summary>
    public int RecipeId { get; set; }

    /// <summary>Meal slot text.</summary>
    public string MealType { get; set; } = string.Empty;

    /// <summary>Recipe display name.</summary>
    public string RecipeName { get; set; } = string.Empty;

    /// <summary>Calories for this portion.</summary>
    public int Calories { get; set; }

    /// <summary>Recipe portion scaling.</summary>
    public double PortionMultiplier { get; set; }
}
