namespace MealPlannerApp.Infrastructure;

/// <summary>
/// Helpers for Monday-based planner weeks.
/// </summary>
public static class WeekDateHelper
{
    /// <summary>
    /// Returns Monday for the current week.
    /// </summary>
    public static DateTime GetCurrentWeekStart()
    {
        return GetWeekStart(DateTime.Today);
    }

    /// <summary>
    /// Returns Monday for the date's week.
    /// </summary>
    public static DateTime GetWeekStart(DateTime date)
    {
        var day = date.Date;
        var diff = (7 + (day.DayOfWeek - DayOfWeek.Monday)) % 7;
        return day.AddDays(-diff);
    }
}
