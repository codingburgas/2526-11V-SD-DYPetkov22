namespace MealPlannerApp.Models;

/// <summary>
/// Review state for shared recipes and templates.
/// </summary>
public enum ApprovalStatus
{
    // Saved privately and not sent to admins.
    Draft,

    // Waiting for an admin decision.
    PendingReview,

    // Visible to everyone.
    Approved,

    // Returned to the owner with feedback.
    Rejected
}
