using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Dtos.MealPlans;

/// <summary>
/// Form values for starting a preset week.
/// </summary>
public class StartPresetMealPlanDto
{
    /// <summary>Monday date for the target week.</summary>
    [Required]
    [DataType(DataType.Date)]
    public DateTime WeekStart { get; set; } = DateTime.Today;

    /// <summary>Selected preset key.</summary>
    [Required]
    public string PresetKey { get; set; } = string.Empty;

    /// <summary>Body weight used for portions.</summary>
    [Display(Name = "Body Weight (kg)")]
    [Range(40, 180)]
    public double BodyWeightKg { get; set; } = 70;
}

/// <summary>
/// Preset plan option shown in the UI.
/// </summary>
public class PresetMealPlanOptionDto
{
    /// <summary>Preset key sent back to the server.</summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>Preset display name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Preset description.</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>Average daily calories at 70 kg.</summary>
    public int AverageDailyCaloriesAtReferenceWeight { get; set; }

    /// <summary>Recipes highlighted for the preset.</summary>
    public IReadOnlyCollection<string> FeaturedRecipes { get; set; } = Array.Empty<string>();

    /// <summary>True when every recipe is vegetarian.</summary>
    public bool IsVegetarian { get; set; }
}
