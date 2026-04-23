using MealPlannerApp.Models;
using MealPlannerApp.Services.Models;

namespace MealPlannerApp.Services.Interfaces;

/// <summary>
/// Defines ingredient catalog operations.
/// </summary>
public interface IIngredientService
{
    /// <summary>Gets all ingredients.</summary>
    Task<IEnumerable<Ingredient>> GetAllIngredients();

    /// <summary>Gets one ingredient by id.</summary>
    Task<Ingredient?> GetIngredientById(int id);

    /// <summary>Creates one ingredient.</summary>
    Task<Ingredient> CreateIngredient(Ingredient ingredient);

    /// <summary>Updates one ingredient.</summary>
    Task<bool> UpdateIngredient(Ingredient ingredient);

    /// <summary>Deletes one ingredient when possible.</summary>
    Task<DeleteOperationResult> DeleteIngredient(int id);
}
