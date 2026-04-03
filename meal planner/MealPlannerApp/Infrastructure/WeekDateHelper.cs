namespace MealPlannerApp.Infrastructure;

public static class WeekDateHelper
{
    public static DateTime GetCurrentWeekStart()
    {
        return GetWeekStart(DateTime.Today);
    }

    public static DateTime GetWeekStart(DateTime date)
    {
        var day = date.Date;
        var diff = (7 + (day.DayOfWeek - DayOfWeek.Monday)) % 7;
        return day.AddDays(-diff);
    }
}
