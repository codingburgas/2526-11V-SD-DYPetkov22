using MealPlannerApp.Models;

namespace MealPlannerApp.Services;

public static class MealPlanMath
{
    public static int CalculateMealCalories(Meal meal)
    {
        return (int)Math.Round(meal.Recipe.Calories * meal.PortionMultiplier, MidpointRounding.AwayFromZero);
    }

    public static int CalculateIngredientQuantity(Meal meal, RecipeIngredient recipeIngredient)
    {
        return (int)Math.Round(recipeIngredient.QuantityInGrams * meal.PortionMultiplier, MidpointRounding.AwayFromZero);
    }

    public static double CalculatePortionMultiplier(double bodyWeightKg)
    {
        return Math.Round(bodyWeightKg / 70.0, 2, MidpointRounding.AwayFromZero);
    }
}
