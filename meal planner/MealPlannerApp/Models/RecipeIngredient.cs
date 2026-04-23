using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Models;

/// <summary>
/// Join row between a recipe and an ingredient quantity.
/// </summary>
public class RecipeIngredient : BaseEntity
{
    /// <summary>Recipe id.</summary>
    public int RecipeId { get; set; }

    /// <summary>Recipe using the ingredient.</summary>
    public Recipe Recipe { get; set; } = null!;

    /// <summary>Ingredient id.</summary>
    public int IngredientId { get; set; }

    /// <summary>Ingredient used by the recipe.</summary>
    public Ingredient Ingredient { get; set; } = null!;

    [Range(1, int.MaxValue)]
    /// <summary>Ingredient amount in grams.</summary>
    public int QuantityInGrams { get; set; }
}
