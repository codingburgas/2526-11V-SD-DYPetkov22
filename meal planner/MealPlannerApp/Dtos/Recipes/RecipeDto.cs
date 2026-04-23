using System.ComponentModel.DataAnnotations;
using MealPlannerApp.Models;

namespace MealPlannerApp.Dtos.Recipes;

/// <summary>
/// Recipe form and display values.
/// </summary>
public class RecipeDto
{
    /// <summary>Recipe id.</summary>
    public int Id { get; set; }

    /// <summary>Owner user id.</summary>
    public int OwnerId { get; set; }

    /// <summary>Owner username.</summary>
    public string OwnerUserName { get; set; } = string.Empty;

    /// <summary>Current review state.</summary>
    public ApprovalStatus ApprovalStatus { get; set; }

    /// <summary>Admin review notes.</summary>
    public string? ReviewNotes { get; set; }

    /// <summary>Recipe display name.</summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>Short recipe description.</summary>
    public string? Description { get; set; }

    /// <summary>Cooking instructions.</summary>
    public string? Instructions { get; set; }

    /// <summary>Cooking time in minutes.</summary>
    [Range(1, 300)]
    public int CookingTime { get; set; }

    /// <summary>Base recipe calories.</summary>
    [Range(0, 10000)]
    public int Calories { get; set; }

    /// <summary>True when the recipe is vegetarian.</summary>
    public bool IsVegetarian { get; set; }

    /// <summary>Ingredient rows entered for the recipe.</summary>
    public List<RecipeIngredientDto> Ingredients { get; set; } = new();
}
