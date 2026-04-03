using System.Security.Claims;

namespace MealPlannerApp.Infrastructure;

public static class ClaimsPrincipalExtensions
{
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
