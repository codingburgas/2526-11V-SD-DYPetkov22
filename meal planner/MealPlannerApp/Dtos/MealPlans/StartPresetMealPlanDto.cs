using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Dtos.MealPlans;

public class StartPresetMealPlanDto
{
    [Required]
    [DataType(DataType.Date)]
    public DateTime WeekStart { get; set; } = DateTime.Today;

    [Required]
    public string PresetKey { get; set; } = string.Empty;

    [Display(Name = "Body Weight (kg)")]
    [Range(40, 180)]
    public double BodyWeightKg { get; set; } = 70;
}

public class PresetMealPlanOptionDto
{
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AverageDailyCaloriesAtReferenceWeight { get; set; }
    public IReadOnlyCollection<string> FeaturedRecipes { get; set; } = Array.Empty<string>();
    public bool IsVegetarian { get; set; }
}
