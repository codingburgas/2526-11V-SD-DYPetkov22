using MealPlannerApp.Dtos.MealPlans;
using MealPlannerApp.Infrastructure;
using MealPlannerApp.Models;
using MealPlannerApp.Services;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MealPlannerApp.Controllers;

[Authorize]
public class MealPlansController : Controller
{
    private readonly IMealPlanService _mealPlanService;
    private readonly IRecipeService _recipeService;

    public MealPlansController(IMealPlanService mealPlanService, IRecipeService recipeService)
    {
        _mealPlanService = mealPlanService;
        _recipeService = recipeService;
    }

    public async Task<IActionResult> Index()
    {
        var mealPlans = await _mealPlanService.GetAllMealPlans(User.GetRequiredUserId());
        var dto = mealPlans.Select(MapToDto).ToList();
        return View(dto);
    }

    public async Task<IActionResult> Details(int id)
    {
        var mealPlan = await _mealPlanService.GetMealPlanById(id, User.GetRequiredUserId());
        if (mealPlan is null)
        {
            return NotFound();
        }

        return View(MapToDto(mealPlan));
    }

    public IActionResult Create()
    {
        return View(new MealPlanDto { Date = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MealPlanDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await _mealPlanService.CreateMealPlan(User.GetRequiredUserId(), MapToEntity(dto));
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var mealPlan = await _mealPlanService.GetMealPlanById(id, User.GetRequiredUserId());
        if (mealPlan is null)
        {
            return NotFound();
        }

        return View(MapToDto(mealPlan));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, MealPlanDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var updated = await _mealPlanService.UpdateMealPlan(User.GetRequiredUserId(), MapToEntity(dto));
        if (!updated)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var mealPlan = await _mealPlanService.GetMealPlanById(id, User.GetRequiredUserId());
        if (mealPlan is null)
        {
            return NotFound();
        }

        return View(MapToDto(mealPlan));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _mealPlanService.DeleteMealPlan(id, User.GetRequiredUserId());
        if (!deleted)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Weekly(DateTime? weekStart = null)
    {
        var model = await BuildWeeklyPlannerModel(weekStart);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartPreset([Bind(Prefix = "StartPresetPlan")] StartPresetMealPlanDto dto)
    {
        if (!ModelState.IsValid)
        {
            var invalidModel = await BuildWeeklyPlannerModel(dto.WeekStart, dto);
            return View(nameof(Weekly), invalidModel);
        }

        try
        {
            await _mealPlanService.StartPresetMealPlan(new StartPresetMealPlanRequest
            {
                UserId = User.GetRequiredUserId(),
                WeekStart = dto.WeekStart,
                PresetKey = dto.PresetKey,
                BodyWeightKg = dto.BodyWeightKg
            });
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            var errorModel = await BuildWeeklyPlannerModel(dto.WeekStart, dto);
            return View(nameof(Weekly), errorModel);
        }

        TempData["SuccessMessage"] = "Starter plan added to the selected week.";
        return RedirectToAction(nameof(Weekly), new { weekStart = dto.WeekStart.ToString("yyyy-MM-dd") });
    }

    public async Task<IActionResult> AddMeal(DateTime? weekStart = null)
    {
        var userId = User.GetRequiredUserId();
        var result = await _mealPlanService.GetWeeklyPlan(userId, weekStart);
        for (var offset = 0; offset < 7; offset++)
        {
            await _mealPlanService.CreateMealPlan(userId, new MealPlan
            {
                Date = result.WeekStart.AddDays(offset)
            });
        }

        result = await _mealPlanService.GetWeeklyPlan(userId, result.WeekStart);

        var model = new AddMealDto
        {
            WeekStart = result.WeekStart
        };

        await PopulateAddMealLookups(result.WeekStart);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddMeal(AddMealDto dto)
    {
        if (!ModelState.IsValid)
        {
            await PopulateAddMealLookups(dto.WeekStart);
            return View(dto);
        }

        var meal = await _mealPlanService.AddMealToPlan(dto.MealPlanId, User.GetRequiredUserId(), User.IsInRole(ApplicationRoles.Admin), new Meal
        {
            RecipeId = dto.RecipeId,
            MealType = dto.MealType,
            PortionMultiplier = dto.PortionMultiplier
        });
        if (meal is null)
        {
            ModelState.AddModelError(string.Empty, "Choose a valid day and a recipe you are allowed to use.");
            await PopulateAddMealLookups(dto.WeekStart);
            return View(dto);
        }

        return RedirectToAction(nameof(Weekly), new { weekStart = dto.WeekStart.ToString("yyyy-MM-dd") });
    }

    private async Task PopulateAddMealLookups(DateTime weekStart)
    {
        var weeklyPlan = await _mealPlanService.GetWeeklyPlan(User.GetRequiredUserId(), weekStart);
        var recipes = await _recipeService.GetAllRecipes(User.GetRequiredUserId(), User.IsInRole(ApplicationRoles.Admin));

        ViewBag.MealPlans = weeklyPlan.MealPlans
            .OrderBy(mp => mp.Date)
            .Select(mp => new SelectListItem
            {
                Value = mp.Id.ToString(),
                Text = $"{mp.Date:ddd, dd MMM yyyy}"
            })
            .ToList();

        ViewBag.Recipes = recipes
            .OrderBy(r => r.Name)
            .Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = $"{r.Name} ({r.Calories} kcal base)"
            })
            .ToList();
    }

    private async Task<WeeklyMealPlannerDto> BuildWeeklyPlannerModel(
        DateTime? weekStart,
        StartPresetMealPlanDto? startPresetPlan = null)
    {
        var result = await _mealPlanService.GetWeeklyPlan(User.GetRequiredUserId(), weekStart);
        var presetPlans = (await _mealPlanService.GetPresetMealPlans())
            .Select(plan => new PresetMealPlanOptionDto
            {
                Key = plan.Key,
                Name = plan.Name,
                Description = plan.Description,
                AverageDailyCaloriesAtReferenceWeight = plan.AverageDailyCaloriesAtReferenceWeight,
                FeaturedRecipes = plan.FeaturedRecipes,
                IsVegetarian = plan.IsVegetarian
            })
            .ToList();

        var startPresetModel = startPresetPlan ?? new StartPresetMealPlanDto
        {
            WeekStart = result.WeekStart,
            BodyWeightKg = 70,
            PresetKey = presetPlans.FirstOrDefault()?.Key ?? string.Empty
        };

        if (startPresetPlan is not null)
        {
            startPresetModel.WeekStart = startPresetPlan.WeekStart == default ? result.WeekStart : startPresetPlan.WeekStart;
            if (string.IsNullOrWhiteSpace(startPresetModel.PresetKey))
            {
                startPresetModel.PresetKey = presetPlans.FirstOrDefault()?.Key ?? string.Empty;
            }
        }

        return BuildWeeklyPlannerDto(result, presetPlans, startPresetModel);
    }

    private static WeeklyMealPlannerDto BuildWeeklyPlannerDto(
        WeeklyMealPlanResult result,
        IReadOnlyCollection<PresetMealPlanOptionDto> presetPlans,
        StartPresetMealPlanDto startPresetPlan)
    {
        var dayMap = result.MealPlans
            .GroupBy(mp => mp.Date.Date)
            .ToDictionary(
                group => group.Key,
                group => group.SelectMany(mp => mp.Meals).OrderBy(m => m.MealType).ToList());

        var days = Enumerable.Range(0, 7)
            .Select(offset =>
            {
                var date = result.WeekStart.AddDays(offset).Date;
                var meals = dayMap.TryGetValue(date, out var dayMeals)
                    ? dayMeals.Select(MapWeeklyMeal).ToList()
                    : new List<WeeklyMealItemDto>();

                return new WeeklyDayDto
                {
                    Date = date,
                    TotalCalories = meals.Sum(m => m.Calories),
                    Meals = meals
                };
            })
            .ToList();

        return new WeeklyMealPlannerDto
        {
            WeekStart = result.WeekStart,
            WeekEnd = result.WeekEnd,
            WeeklyTotalCalories = result.WeeklyTotalCalories,
            Days = days,
            MostUsedIngredients = result.MostUsedIngredients
                .Select(i => new MostUsedIngredientDto
                {
                    Name = i.Name,
                    UsageCount = i.UsageCount,
                    TotalQuantityInGrams = i.TotalQuantityInGrams
                })
                .ToList(),
            PresetPlans = presetPlans,
            StartPresetPlan = startPresetPlan
        };
    }

    private static WeeklyMealItemDto MapWeeklyMeal(Meal meal)
    {
        return new WeeklyMealItemDto
        {
            MealType = meal.MealType.ToString(),
            RecipeName = meal.Recipe.Name,
            Calories = MealPlanMath.CalculateMealCalories(meal),
            PortionMultiplier = meal.PortionMultiplier
        };
    }

    private static MealPlanDto MapToDto(MealPlan mealPlan)
    {
        var mealItems = mealPlan.Meals
            .OrderBy(m => m.MealType)
            .Select(m => new MealPlanMealDto
            {
                MealType = m.MealType.ToString(),
                RecipeName = m.Recipe?.Name ?? "Unknown",
                Calories = m.Recipe is null ? 0 : MealPlanMath.CalculateMealCalories(m),
                PortionMultiplier = m.PortionMultiplier
            })
            .ToList();

        var ingredientUsage = mealPlan.Meals
            .Where(m => m.Recipe is not null)
            .SelectMany(m => m.Recipe.RecipeIngredients.Select(ri => new
            {
                IngredientName = ri.Ingredient.Name,
                QuantityInGrams = MealPlanMath.CalculateIngredientQuantity(m, ri)
            }))
            .GroupBy(x => x.IngredientName)
            .Select(g => new MostUsedIngredientSummaryDto
            {
                Name = g.Key,
                UsageCount = g.Count(),
                TotalQuantityInGrams = g.Sum(x => x.QuantityInGrams)
            })
            .OrderByDescending(x => x.UsageCount)
            .ThenByDescending(x => x.TotalQuantityInGrams)
            .Take(5)
            .ToList();

        return new MealPlanDto
        {
            Id = mealPlan.Id,
            Date = mealPlan.Date,
            MealsCount = mealItems.Count,
            TotalCalories = mealItems.Sum(m => m.Calories),
            Meals = mealItems,
            MostUsedIngredients = ingredientUsage
        };
    }

    private static MealPlan MapToEntity(MealPlanDto dto)
    {
        return new MealPlan
        {
            Id = dto.Id,
            Date = dto.Date
        };
    }
}
