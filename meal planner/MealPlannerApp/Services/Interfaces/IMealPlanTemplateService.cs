using MealPlannerApp.Models;

namespace MealPlannerApp.Services.Interfaces;

public interface IMealPlanTemplateService
{
    Task<IReadOnlyCollection<MealPlanTemplate>> GetVisibleTemplates(int? currentUserId, bool isAdmin);
    Task<MealPlanTemplate?> GetTemplateById(int id, int? currentUserId, bool isAdmin);
    Task<MealPlanTemplate> CreateFromWeek(int ownerId, DateTime weekStart, string name, string? description, bool submitForReview);
    Task<bool> SubmitForReview(int id, int ownerId, bool isAdmin);
    Task<IReadOnlyCollection<MealPlanTemplate>> GetPendingTemplates();
    Task<bool> ApproveTemplate(int id);
    Task<bool> RejectTemplate(int id, string? reviewNotes);
    Task<bool> ApplyTemplateToWeek(int id, int currentUserId, DateTime weekStart, bool isAdmin);
}
