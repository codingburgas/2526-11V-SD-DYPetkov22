using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Models;

/// <summary>
/// Cookable recipe with ingredients and moderation state.
/// </summary>
public class Recipe : BaseEntity
{
    [Required]
    /// <summary>Recipe display name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Short recipe description.</summary>
    public string? Description { get; set; }

    /// <summary>Cooking instructions.</summary>
    public string? Instructions { get; set; }

    [Range(1, 300)]
    /// <summary>Cooking time in minutes.</summary>
    public int CookingTime { get; set; }

    /// <summary>Base recipe calories.</summary>
    public int Calories { get; set; }

    /// <summary>True when the recipe has no meat or fish.</summary>
    public bool IsVegetarian { get; set; }

    /// <summary>Owner user id.</summary>
    public int OwnerId { get; set; }

    /// <summary>Owner user.</summary>
    public User Owner { get; set; } = null!;

    /// <summary>Current review state.</summary>
    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Draft;

    [StringLength(500)]
    /// <summary>Admin feedback when rejected.</summary>
    public string? ReviewNotes { get; set; }

    /// <summary>UTC time when submitted.</summary>
    public DateTime? SubmittedAt { get; set; }

    /// <summary>UTC time when reviewed.</summary>
    public DateTime? ReviewedAt { get; set; }

    /// <summary>Ingredient quantities used by this recipe.</summary>
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    /// <summary>Planned meals using this recipe.</summary>
    public ICollection<Meal> Meals { get; set; } = new List<Meal>();

    /// <summary>Template meals using this recipe.</summary>
    public ICollection<MealPlanTemplateMeal> MealPlanTemplateMeals { get; set; } = new List<MealPlanTemplateMeal>();
}
