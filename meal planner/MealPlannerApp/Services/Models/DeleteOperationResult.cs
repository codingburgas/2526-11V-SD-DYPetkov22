namespace MealPlannerApp.Services.Models;

/// <summary>
/// Result of a delete attempt.
/// </summary>
public enum DeleteOperationResult
{
    // Row was deleted.
    Deleted,

    // Row does not exist.
    NotFound,

    // Current user cannot delete it.
    Forbidden,

    // Other rows still use it.
    InUse
}
