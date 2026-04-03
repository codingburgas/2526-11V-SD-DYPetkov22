namespace MealPlannerApp.Services.Models;

public enum DeleteOperationResult
{
    Deleted,
    NotFound,
    Forbidden,
    InUse
}
