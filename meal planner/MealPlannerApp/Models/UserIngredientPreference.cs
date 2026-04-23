namespace MealPlannerApp.Models;

/// <summary>
/// User-specific ingredient exclusion or allergy.
/// </summary>
public class UserIngredientPreference : BaseEntity
{
    /// <summary>User id.</summary>
    public int UserId { get; set; }

    /// <summary>User who owns the preference.</summary>
    public User User { get; set; } = null!;

    /// <summary>Ingredient id.</summary>
    public int IngredientId { get; set; }

    /// <summary>Ingredient being blocked.</summary>
    public Ingredient Ingredient { get; set; } = null!;

    /// <summary>Exclusion or allergy type.</summary>
    public UserIngredientPreferenceType PreferenceType { get; set; }
}
