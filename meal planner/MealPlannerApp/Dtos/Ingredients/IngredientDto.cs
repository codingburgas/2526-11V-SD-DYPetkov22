using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Dtos.Ingredients;

/// <summary>
/// Ingredient form and list values.
/// </summary>
public class IngredientDto
{
    /// <summary>Ingredient id.</summary>
    public int Id { get; set; }

    /// <summary>Ingredient display name.</summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>Calories per 100 grams.</summary>
    [Range(0, 1000)]
    public int CaloriesPer100g { get; set; }

    /// <summary>Protein grams per 100 grams.</summary>
    [Display(Name = "Protein / 100g")]
    [Range(0, 100)]
    public double ProteinPer100g { get; set; }

    /// <summary>Carbs grams per 100 grams.</summary>
    [Display(Name = "Carbs / 100g")]
    [Range(0, 100)]
    public double CarbsPer100g { get; set; }

    /// <summary>Fat grams per 100 grams.</summary>
    [Display(Name = "Fat / 100g")]
    [Range(0, 100)]
    public double FatPer100g { get; set; }
}
