namespace MealPlannerApp.Services.Models;

public class PresetMealPlanOptionResult
{
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AverageDailyCaloriesAtReferenceWeight { get; set; }
    public IReadOnlyCollection<string> FeaturedRecipes { get; set; } = Array.Empty<string>();
    public bool IsVegetarian { get; set; }
}
