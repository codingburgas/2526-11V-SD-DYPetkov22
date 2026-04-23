using MealPlannerApp.Models;

namespace MealPlannerApp.Services.Interfaces;

/// <summary>
/// Defines shared meal plan template operations.
/// </summary>
public interface IMealPlanTemplateService
{
    /// <summary>Gets templates visible to a visitor.</summary>
    Task<IReadOnlyCollection<MealPlanTemplate>> GetVisibleTemplates(int? currentUserId, bool isAdmin);

    /// <summary>Gets one visible template.</summary>
    Task<MealPlanTemplate?> GetTemplateById(int id, int? currentUserId, bool isAdmin);

    /// <summary>Creates a template from a week.</summary>
    Task<MealPlanTemplate> CreateFromWeek(int ownerId, DateTime weekStart, string name, string? description, bool submitForReview);

    /// <summary>Submits a template for review.</summary>
    Task<bool> SubmitForReview(int id, int ownerId, bool isAdmin);

    /// <summary>Gets templates waiting for review.</summary>
    Task<IReadOnlyCollection<MealPlanTemplate>> GetPendingTemplates();

    /// <summary>Approves a template.</summary>
    Task<bool> ApproveTemplate(int id);

    /// <summary>Rejects a template.</summary>
    Task<bool> RejectTemplate(int id, string? reviewNotes);

    /// <summary>Applies a template to a week.</summary>
    Task<bool> ApplyTemplateToWeek(int id, int currentUserId, DateTime weekStart, bool isAdmin);
}
