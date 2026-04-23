namespace MealPlannerApp.Models;

/// <summary>
/// App-level user role flag.
/// </summary>
public enum UserRole
{
    // Can moderate and manage catalogs.
    Admin,

    // Can plan meals and create own content.
    User
}
