using System.Security.Claims;

namespace MealPlannerApp.Infrastructure;

/// <summary>
/// Helpers for reading authenticated user claims.
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Returns the required integer user id claim.
    /// </summary>
    public static int GetRequiredUserId(this ClaimsPrincipal principal)
    {
        var value = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(value, out var userId))
        {
            return userId;
        }

        throw new InvalidOperationException("The authenticated user id claim is missing.");
    }
}
