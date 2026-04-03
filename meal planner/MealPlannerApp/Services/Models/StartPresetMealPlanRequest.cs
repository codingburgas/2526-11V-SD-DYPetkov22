namespace MealPlannerApp.Services.Models;

public class StartPresetMealPlanRequest
{
    public int UserId { get; set; }
    public DateTime WeekStart { get; set; }
    public string PresetKey { get; set; } = string.Empty;
    public double BodyWeightKg { get; set; }
}
