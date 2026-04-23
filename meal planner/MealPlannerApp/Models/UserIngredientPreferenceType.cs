namespace MealPlannerApp.Models;

/// <summary>
/// Type of ingredient block saved by a user.
/// </summary>
public enum UserIngredientPreferenceType
{
    // Avoid this ingredient when planning.
    Excluded = 0,

    // Treat this ingredient as a hard allergy block.
    Allergy = 1
}
