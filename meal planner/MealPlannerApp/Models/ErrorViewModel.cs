namespace MealPlannerApp.Models;

/// <summary>
/// Data shown on the error page.
/// </summary>
public class ErrorViewModel
{
    /// <summary>Request id used for troubleshooting.</summary>
    public string? RequestId { get; set; }

    /// <summary>True when an id can be displayed.</summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
