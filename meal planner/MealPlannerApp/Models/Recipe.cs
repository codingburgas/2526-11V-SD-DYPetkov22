using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Models;

public class Recipe : BaseEntity
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
    public string? Instructions { get; set; }

    [Range(1, 300)]
    public int CookingTime { get; set; }

    public int Calories { get; set; }

    public bool IsVegetarian { get; set; }

    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!;

    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Draft;

    [StringLength(500)]
    public string? ReviewNotes { get; set; }

    public DateTime? SubmittedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }

    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    public ICollection<Meal> Meals { get; set; } = new List<Meal>();
    public ICollection<MealPlanTemplateMeal> MealPlanTemplateMeals { get; set; } = new List<MealPlanTemplateMeal>();
}
