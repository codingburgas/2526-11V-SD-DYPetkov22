using MealPlannerApp.Data;
using MealPlannerApp.Infrastructure;
using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApp.Services;

public class MealPlanService : IMealPlanService
{
    private static readonly IReadOnlyCollection<PresetMealPlanDefinition> PresetMealPlanDefinitions =
    [
        new(
            "balanced-starter",
            "Balanced Starter Week",
            "Simple mixed meals with repeated favorites so a beginner can start a full week without planning from scratch.",
            [
                new(0, MealType.Breakfast, 4),
                new(0, MealType.Lunch, 1),
                new(0, MealType.Dinner, 5),
                new(1, MealType.Breakfast, 3),
                new(1, MealType.Lunch, 7),
                new(1, MealType.Dinner, 8),
                new(2, MealType.Breakfast, 4),
                new(2, MealType.Lunch, 1),
                new(2, MealType.Dinner, 6),
                new(3, MealType.Breakfast, 3),
                new(3, MealType.Lunch, 5),
                new(3, MealType.Dinner, 8),
                new(4, MealType.Breakfast, 4),
                new(4, MealType.Lunch, 7),
                new(4, MealType.Dinner, 1),
                new(5, MealType.Breakfast, 3),
                new(5, MealType.Lunch, 6),
                new(5, MealType.Dinner, 5),
                new(6, MealType.Breakfast, 4),
                new(6, MealType.Lunch, 1),
                new(6, MealType.Dinner, 7)
            ]),
        new(
            "high-protein-starter",
            "High-Protein Starter Week",
            "Built around eggs, chicken, and salmon for people who want an easy higher-protein starting template.",
            [
                new(0, MealType.Breakfast, 3),
                new(0, MealType.Lunch, 1),
                new(0, MealType.Dinner, 5),
                new(1, MealType.Breakfast, 4),
                new(1, MealType.Lunch, 7),
                new(1, MealType.Dinner, 1),
                new(2, MealType.Breakfast, 3),
                new(2, MealType.Lunch, 1),
                new(2, MealType.Dinner, 5),
                new(3, MealType.Breakfast, 4),
                new(3, MealType.Lunch, 7),
                new(3, MealType.Dinner, 5),
                new(4, MealType.Breakfast, 3),
                new(4, MealType.Lunch, 1),
                new(4, MealType.Dinner, 7),
                new(5, MealType.Breakfast, 4),
                new(5, MealType.Lunch, 1),
                new(5, MealType.Dinner, 5),
                new(6, MealType.Breakfast, 3),
                new(6, MealType.Lunch, 7),
                new(6, MealType.Dinner, 5)
            ]),
        new(
            "vegetarian-starter",
            "Vegetarian Starter Week",
            "An easier vegetarian week with a small recipe rotation and straightforward prep for each day.",
            [
                new(0, MealType.Breakfast, 4),
                new(0, MealType.Lunch, 2),
                new(0, MealType.Dinner, 6),
                new(1, MealType.Breakfast, 3),
                new(1, MealType.Lunch, 8),
                new(1, MealType.Dinner, 2),
                new(2, MealType.Breakfast, 4),
                new(2, MealType.Lunch, 6),
                new(2, MealType.Dinner, 3),
                new(3, MealType.Breakfast, 3),
                new(3, MealType.Lunch, 2),
                new(3, MealType.Dinner, 8),
                new(4, MealType.Breakfast, 4),
                new(4, MealType.Lunch, 6),
                new(4, MealType.Dinner, 2),
                new(5, MealType.Breakfast, 3),
                new(5, MealType.Lunch, 8),
                new(5, MealType.Dinner, 6),
                new(6, MealType.Breakfast, 4),
                new(6, MealType.Lunch, 2),
                new(6, MealType.Dinner, 6)
            ])
    ];

    private readonly ApplicationDbContext _dbContext;

    public MealPlanService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MealPlan>> GetAllMealPlans(int userId)
    {
        return await _dbContext.MealPlans
            .Where(mp => mp.UserId == userId)
            .Include(mp => mp.User)
            .Include(mp => mp.Meals)
            .ThenInclude(m => m.Recipe)
            .ThenInclude(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .OrderBy(mp => mp.Date)
            .ToListAsync();
    }

    public async Task<MealPlan?> GetMealPlanById(int id, int userId)
    {
        return await _dbContext.MealPlans
            .Where(mp => mp.UserId == userId)
            .Include(mp => mp.User)
            .Include(mp => mp.Meals)
            .ThenInclude(m => m.Recipe)
            .ThenInclude(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .FirstOrDefaultAsync(mp => mp.Id == id);
    }

    public async Task<MealPlan> CreateMealPlan(int userId, MealPlan mealPlan)
    {
        var targetDate = mealPlan.Date.Date;
        var existingMealPlan = await _dbContext.MealPlans
            .FirstOrDefaultAsync(mp =>
                mp.UserId == userId &&
                mp.Date >= targetDate &&
                mp.Date < targetDate.AddDays(1));
        if (existingMealPlan is not null)
        {
            return existingMealPlan;
        }

        mealPlan.UserId = userId;
        mealPlan.Date = targetDate;
        mealPlan.CreatedAt = DateTime.UtcNow;
        _dbContext.MealPlans.Add(mealPlan);
        await _dbContext.SaveChangesAsync();
        return mealPlan;
    }

    public async Task<bool> UpdateMealPlan(int userId, MealPlan mealPlan)
    {
        var existingMealPlan = await _dbContext.MealPlans
            .FirstOrDefaultAsync(mp => mp.Id == mealPlan.Id && mp.UserId == userId);
        if (existingMealPlan is null)
        {
            return false;
        }

        existingMealPlan.Date = mealPlan.Date.Date;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteMealPlan(int id, int userId)
    {
        var mealPlan = await _dbContext.MealPlans
            .FirstOrDefaultAsync(mp => mp.Id == id && mp.UserId == userId);
        if (mealPlan is null)
        {
            return false;
        }

        _dbContext.MealPlans.Remove(mealPlan);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Meal?> AddMealToPlan(int mealPlanId, int userId, bool isAdmin, Meal meal)
    {
        var mealPlanExists = await _dbContext.MealPlans
            .AnyAsync(mp => mp.Id == mealPlanId && mp.UserId == userId);
        if (!mealPlanExists)
        {
            return null;
        }

        var recipeAccess = await _dbContext.Recipes
            .Where(recipe => recipe.Id == meal.RecipeId)
            .Select(recipe => new
            {
                recipe.Id,
                recipe.OwnerId,
                recipe.ApprovalStatus
            })
            .FirstOrDefaultAsync();
        if (recipeAccess is null)
        {
            return null;
        }

        var canUseRecipe = isAdmin
            || recipeAccess.ApprovalStatus == ApprovalStatus.Approved
            || recipeAccess.OwnerId == userId;
        if (!canUseRecipe)
        {
            return null;
        }

        meal.MealPlanId = mealPlanId;
        meal.RecipeId = recipeAccess.Id;
        meal.CreatedAt = DateTime.UtcNow;
        if (meal.PortionMultiplier <= 0)
        {
            meal.PortionMultiplier = 1.0;
        }
        else
        {
            meal.PortionMultiplier = Math.Clamp(meal.PortionMultiplier, 0.5, 3.0);
        }

        _dbContext.Meals.Add(meal);
        await _dbContext.SaveChangesAsync();
        return meal;
    }

    public async Task<WeeklyMealPlanResult> GetWeeklyPlan(int userId, DateTime? weekStart = null)
    {
        var startDate = weekStart?.Date ?? WeekDateHelper.GetCurrentWeekStart();
        var endDate = startDate.AddDays(7);

        var mealPlans = await _dbContext.MealPlans
            .Where(mp => mp.UserId == userId && mp.Date >= startDate && mp.Date < endDate)
            .Include(mp => mp.Meals)
            .ThenInclude(m => m.Recipe)
            .ThenInclude(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .ToListAsync();

        var dailyCalories = mealPlans
            .SelectMany(mp => mp.Meals.Select(m => new { Date = mp.Date.Date, Calories = MealPlanMath.CalculateMealCalories(m) }))
            .GroupBy(x => x.Date)
            .Select(g => new DailyCaloriesResult
            {
                Date = g.Key,
                TotalCalories = g.Sum(x => x.Calories)
            })
            .OrderBy(x => x.Date)
            .ToList();

        var weeklyCalories = dailyCalories.Sum(x => x.TotalCalories);
        var mostUsedIngredients = mealPlans
            .SelectMany(mp => mp.Meals)
            .SelectMany(m => m.Recipe.RecipeIngredients.Select(ri => new
            {
                ri.Ingredient.Name,
                QuantityInGrams = MealPlanMath.CalculateIngredientQuantity(m, ri)
            }))
            .GroupBy(x => x.Name)
            .Select(g => new MostUsedIngredientResult
            {
                Name = g.Key,
                UsageCount = g.Count(),
                TotalQuantityInGrams = g.Sum(x => x.QuantityInGrams)
            })
            .OrderByDescending(x => x.UsageCount)
            .ThenByDescending(x => x.TotalQuantityInGrams)
            .Take(5)
            .ToList();

        return new WeeklyMealPlanResult
        {
            WeekStart = startDate,
            WeekEnd = endDate.AddDays(-1),
            WeeklyTotalCalories = weeklyCalories,
            DailyCalories = dailyCalories,
            MostUsedIngredients = mostUsedIngredients,
            MealPlans = mealPlans
        };
    }

    public async Task<IReadOnlyCollection<PresetMealPlanOptionResult>> GetPresetMealPlans()
    {
        var recipes = await GetPresetRecipes();

        return PresetMealPlanDefinitions
            .Where(plan => plan.Meals.All(meal => recipes.ContainsKey(meal.RecipeId)))
            .Select(plan => new PresetMealPlanOptionResult
            {
                Key = plan.Key,
                Name = plan.Name,
                Description = plan.Description,
                AverageDailyCaloriesAtReferenceWeight = (int)Math.Round(
                    plan.Meals.Sum(meal => recipes[meal.RecipeId].Calories) / 7.0,
                    MidpointRounding.AwayFromZero),
                FeaturedRecipes = plan.Meals
                    .Select(meal => recipes[meal.RecipeId].Name)
                    .Distinct()
                    .Take(3)
                    .ToArray(),
                IsVegetarian = plan.Meals.All(meal => recipes[meal.RecipeId].IsVegetarian)
            })
            .ToList();
    }

    public async Task StartPresetMealPlan(StartPresetMealPlanRequest request)
    {
        var presetPlan = PresetMealPlanDefinitions.FirstOrDefault(plan => plan.Key == request.PresetKey);
        if (presetPlan is null)
        {
            throw new InvalidOperationException("The selected starter plan is no longer available.");
        }

        var userExists = await _dbContext.Users.AnyAsync(u => u.Id == request.UserId);
        if (!userExists)
        {
            throw new InvalidOperationException("The selected user does not exist.");
        }

        var requiredRecipeIds = presetPlan.Meals.Select(meal => meal.RecipeId).Distinct().ToList();
        var availableRecipeIds = await _dbContext.Recipes
            .Where(recipe => requiredRecipeIds.Contains(recipe.Id) && recipe.ApprovalStatus == ApprovalStatus.Approved)
            .Select(recipe => recipe.Id)
            .ToListAsync();

        if (availableRecipeIds.Count != requiredRecipeIds.Count)
        {
            throw new InvalidOperationException("This starter plan cannot be started because one or more required recipes are missing.");
        }

        var weekStart = request.WeekStart.Date;
        var weekEnd = weekStart.AddDays(7);
        var portionMultiplier = MealPlanMath.CalculatePortionMultiplier(request.BodyWeightKg);

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        var existingMealPlans = await _dbContext.MealPlans
            .Where(mp => mp.UserId == request.UserId && mp.Date >= weekStart && mp.Date < weekEnd)
            .Include(mp => mp.Meals)
            .OrderBy(mp => mp.Date)
            .ThenBy(mp => mp.Id)
            .ToListAsync();

        var duplicateMealPlans = existingMealPlans
            .GroupBy(mp => mp.Date.Date)
            .SelectMany(group => group.Skip(1))
            .ToList();

        if (duplicateMealPlans.Count > 0)
        {
            _dbContext.MealPlans.RemoveRange(duplicateMealPlans);
        }

        var mealPlansByDate = existingMealPlans
            .GroupBy(mp => mp.Date.Date)
            .ToDictionary(group => group.Key, group => group.First());

        var existingMeals = mealPlansByDate.Values.SelectMany(mp => mp.Meals).ToList();
        if (existingMeals.Count > 0)
        {
            _dbContext.Meals.RemoveRange(existingMeals);
        }

        for (var offset = 0; offset < 7; offset++)
        {
            var date = weekStart.AddDays(offset).Date;
            if (mealPlansByDate.ContainsKey(date))
            {
                continue;
            }

            var mealPlan = new MealPlan
            {
                UserId = request.UserId,
                Date = date,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.MealPlans.Add(mealPlan);
            mealPlansByDate[date] = mealPlan;
        }

        await _dbContext.SaveChangesAsync();

        var mealsToAdd = presetPlan.Meals
            .Select(meal => new Meal
            {
                MealPlanId = mealPlansByDate[weekStart.AddDays(meal.DayOffset).Date].Id,
                RecipeId = meal.RecipeId,
                MealType = meal.MealType,
                PortionMultiplier = portionMultiplier,
                CreatedAt = DateTime.UtcNow
            })
            .ToList();

        _dbContext.Meals.AddRange(mealsToAdd);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    private async Task<Dictionary<int, Recipe>> GetPresetRecipes()
    {
        var recipeIds = PresetMealPlanDefinitions
            .SelectMany(plan => plan.Meals)
            .Select(meal => meal.RecipeId)
            .Distinct()
            .ToList();

        return await _dbContext.Recipes
            .Where(recipe => recipeIds.Contains(recipe.Id) && recipe.ApprovalStatus == ApprovalStatus.Approved)
            .ToDictionaryAsync(recipe => recipe.Id);
    }

    private sealed record PresetMealPlanDefinition(
        string Key,
        string Name,
        string Description,
        IReadOnlyCollection<PresetMealPlanEntry> Meals);

    private sealed record PresetMealPlanEntry(int DayOffset, MealType MealType, int RecipeId);
}
