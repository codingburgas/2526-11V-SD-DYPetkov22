namespace MealPlannerApp.Infrastructure;

/// <summary>
/// Central names for app authorization roles.
/// </summary>
public static class ApplicationRoles
{
    // Role allowed to manage catalogs and moderation.
    public const string Admin = "Admin";

    // Role used for normal signed-in users.
    public const string User = "User";

    // All roles that must be seeded.
    public static readonly string[] All = [Admin, User];
}
