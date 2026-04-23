using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Models;

/// <summary>
/// Application user with saved planner preferences.
/// </summary>
public class User : IdentityUser<int>
{
    /// <summary>UTC time when the user was created.</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>App role used for business rules.</summary>
    public UserRole Role { get; set; } = UserRole.User;

    [Range(1, 3)]
    /// <summary>Saved meals per day preference.</summary>
    public int PreferredMealsPerDay { get; set; } = 3;

    [Range(40, 180)]
    /// <summary>Saved body weight in kilograms.</summary>
    public double BodyWeightKg { get; set; } = 70;

    [Range(40, 300)]
    /// <summary>Saved daily protein target.</summary>
    public double ProteinTargetGrams { get; set; } = 126;

    [Range(20, 500)]
    /// <summary>Saved daily carbs target.</summary>
    public double CarbsTargetGrams { get; set; } = 273;

    [Range(20, 200)]
    /// <summary>Saved daily fat target.</summary>
    public double FatTargetGrams { get; set; } = 56;

    [StringLength(500)]
    /// <summary>Free-text foods to avoid.</summary>
    public string? ExcludedFoods { get; set; }

    /// <summary>Meal plans owned by the user.</summary>
    public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();

    /// <summary>Recipes owned by the user.</summary>
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    /// <summary>Templates owned by the user.</summary>
    public ICollection<MealPlanTemplate> MealPlanTemplates { get; set; } = new List<MealPlanTemplate>();

    /// <summary>Ingredient preferences owned by the user.</summary>
    public ICollection<UserIngredientPreference> IngredientPreferences { get; set; } = new List<UserIngredientPreference>();
}
