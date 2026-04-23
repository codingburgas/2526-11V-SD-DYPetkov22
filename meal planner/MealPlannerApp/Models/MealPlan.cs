using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Models;

/// <summary>
/// A user's plan for one calendar day.
/// </summary>
public class MealPlan : BaseEntity
{
    /// <summary>Owner user id.</summary>
    public int UserId { get; set; }

    /// <summary>Owner user.</summary>
    public User User { get; set; } = null!;

    [DataType(DataType.Date)]
    /// <summary>Calendar date for this plan.</summary>
    public DateTime Date { get; set; }

    /// <summary>Meals scheduled for this date.</summary>
    public ICollection<Meal> Meals { get; set; } = new List<Meal>();
}
