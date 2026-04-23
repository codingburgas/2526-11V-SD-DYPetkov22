namespace MealPlannerApp.Models;

/// <summary>
/// Common fields for app entities.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>Primary database id.</summary>
    public int Id { get; set; }

    /// <summary>UTC time when the row was created.</summary>
    public DateTime CreatedAt { get; set; }
}
