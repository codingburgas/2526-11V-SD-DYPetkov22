namespace MealPlannerApp.Services.Models;

/// <summary>
/// Request values for starting a preset week.
/// </summary>
public class StartPresetMealPlanRequest
{
    /// <summary>User receiving the preset plan.</summary>
    public int UserId { get; set; }

    /// <summary>Monday date for the target week.</summary>
    public DateTime WeekStart { get; set; }

    /// <summary>Preset plan key.</summary>
    public string PresetKey { get; set; } = string.Empty;

    /// <summary>Body weight used for portions.</summary>
    public double BodyWeightKg { get; set; }
}
