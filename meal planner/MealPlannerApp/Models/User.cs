using Microsoft.AspNetCore.Identity;

namespace MealPlannerApp.Models;

public class User : IdentityUser<int>
{
    public DateTime CreatedAt { get; set; }

    public UserRole Role { get; set; } = UserRole.User;

    public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    public ICollection<MealPlanTemplate> MealPlanTemplates { get; set; } = new List<MealPlanTemplate>();
}
