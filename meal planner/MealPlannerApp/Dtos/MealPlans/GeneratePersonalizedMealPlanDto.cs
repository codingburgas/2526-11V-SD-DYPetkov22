using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Dtos.MealPlans;

/// <summary>
/// Form values for generating a personalized week.
/// </summary>
public class GeneratePersonalizedMealPlanDto
{
    /// <summary>Monday date for the target week.</summary>
    [Required]
    [DataType(DataType.Date)]
    public DateTime WeekStart { get; set; } = DateTime.Today;

    /// <summary>Number of meals generated per day.</summary>
    [Display(Name = "Meals Per Day")]
    [Range(1, 3)]
    public int MealsPerDay { get; set; } = 3;

    /// <summary>Body weight used for defaults.</summary>
    [Display(Name = "Body Weight (kg)")]
    [Range(40, 180)]
    public double BodyWeightKg { get; set; } = 70;

    /// <summary>Daily protein target.</summary>
    [Display(Name = "Protein Target (g/day)")]
    [Range(40, 300)]
    public double ProteinTargetGrams { get; set; } = 126;

    /// <summary>Daily carbs target.</summary>
    [Display(Name = "Carbs Target (g/day)")]
    [Range(20, 500)]
    public double CarbsTargetGrams { get; set; } = 273;

    /// <summary>Daily fat target.</summary>
    [Display(Name = "Fat Target (g/day)")]
    [Range(20, 200)]
    public double FatTargetGrams { get; set; } = 56;

    /// <summary>Free-text foods to avoid.</summary>
    [Display(Name = "Foods You Do Not Eat")]
    [StringLength(500)]
    public string? ExcludedFoods { get; set; }

    /// <summary>Ingredient ids to avoid.</summary>
    [Display(Name = "Excluded Ingredients")]
    public List<int> ExcludedIngredientIds { get; set; } = [];

    /// <summary>Ingredient ids treated as allergies.</summary>
    [Display(Name = "Allergy Ingredients")]
    public List<int> AllergyIngredientIds { get; set; } = [];
}
