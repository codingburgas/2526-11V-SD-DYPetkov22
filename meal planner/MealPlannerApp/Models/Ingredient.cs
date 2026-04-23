using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Models;

/// <summary>
/// Food item with nutrition per 100 grams.
/// </summary>
public class Ingredient : BaseEntity
{
    [Required]
    /// <summary>Ingredient display name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Calories per 100 grams.</summary>
    public int CaloriesPer100g { get; set; }

    /// <summary>Protein grams per 100 grams.</summary>
    public double ProteinPer100g { get; set; }

    /// <summary>Carb grams per 100 grams.</summary>
    public double CarbsPer100g { get; set; }

    /// <summary>Fat grams per 100 grams.</summary>
    public double FatPer100g { get; set; }

    /// <summary>Recipes that use this ingredient.</summary>
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    /// <summary>User preferences tied to this ingredient.</summary>
    public ICollection<UserIngredientPreference> UserPreferences { get; set; } = new List<UserIngredientPreference>();
}
