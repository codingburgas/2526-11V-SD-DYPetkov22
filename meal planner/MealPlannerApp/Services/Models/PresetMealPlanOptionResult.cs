namespace MealPlannerApp.Services.Models;

/// <summary>
/// Starter plan option returned by the service.
/// </summary>
public class PresetMealPlanOptionResult
{
    /// <summary>Preset key.</summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>Preset display name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Preset description.</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>Average daily calories at 70 kg.</summary>
    public int AverageDailyCaloriesAtReferenceWeight { get; set; }

    /// <summary>Highlighted recipes.</summary>
    public IReadOnlyCollection<string> FeaturedRecipes { get; set; } = Array.Empty<string>();

    /// <summary>True when every recipe is vegetarian.</summary>
    public bool IsVegetarian { get; set; }
}
