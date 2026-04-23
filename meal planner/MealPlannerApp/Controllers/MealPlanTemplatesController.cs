using MealPlannerApp.Dtos.MealPlanTemplates;
using MealPlannerApp.Dtos.Moderation;
using MealPlannerApp.Infrastructure;
using MealPlannerApp.Models;
using MealPlannerApp.Services;
using MealPlannerApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApp.Controllers;

/// <summary>
/// Handles shared weekly meal plan templates.
/// </summary>
public class MealPlanTemplatesController : Controller
{
    private readonly IMealPlanTemplateService _mealPlanTemplateService;

    /// <summary>
    /// Receives the template service.
    /// </summary>
    public MealPlanTemplatesController(IMealPlanTemplateService mealPlanTemplateService)
    {
        _mealPlanTemplateService = mealPlanTemplateService;
    }

    /// <summary>
    /// Lists visible templates.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        var templates = await _mealPlanTemplateService.GetVisibleTemplates(GetCurrentUserId(), IsAdmin());
        return View(templates.Select(MapToDto).ToList());
    }

    /// <summary>
    /// Shows one visible template.
    /// </summary>
    public async Task<IActionResult> Details(int id)
    {
        var template = await _mealPlanTemplateService.GetTemplateById(id, GetCurrentUserId(), IsAdmin());
        if (template is null)
        {
            return NotFound();
        }

        return View(MapToDto(template));
    }

    /// <summary>
    /// Shows the form to save the current week.
    /// </summary>
    [Authorize]
    public IActionResult CreateFromWeek(DateTime? weekStart = null)
    {
        var targetWeek = weekStart.HasValue
            ? WeekDateHelper.GetWeekStart(weekStart.Value)
            : WeekDateHelper.GetCurrentWeekStart();
        return View(new CreateMealPlanTemplateDto
        {
            WeekStart = targetWeek,
            Name = $"Week of {targetWeek:dd MMM yyyy}"
        });
    }

    /// <summary>
    /// Saves the current week as a template.
    /// </summary>
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateFromWeek(CreateMealPlanTemplateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            var template = await _mealPlanTemplateService.CreateFromWeek(
                User.GetRequiredUserId(),
                dto.WeekStart,
                dto.Name,
                dto.Description,
                dto.SubmitForReview);

            TempData["SuccessMessage"] = dto.SubmitForReview
                ? "Plan template submitted for admin review."
                : "Plan template saved as a private draft.";
            return RedirectToAction(nameof(Details), new { id = template.Id });
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
    }

    /// <summary>
    /// Sends a template to admin review.
    /// </summary>
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitForReview(int id)
    {
        var submitted = await _mealPlanTemplateService.SubmitForReview(id, User.GetRequiredUserId(), IsAdmin());
        if (!submitted)
        {
            return Forbid();
        }

        TempData["SuccessMessage"] = "Plan template submitted for admin review.";
        return RedirectToAction(nameof(Details), new { id });
    }

    /// <summary>
    /// Applies a template to the selected week.
    /// </summary>
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Apply(int id, DateTime? weekStart = null)
    {
        var targetWeek = weekStart.HasValue
            ? WeekDateHelper.GetWeekStart(weekStart.Value)
            : WeekDateHelper.GetCurrentWeekStart();
        bool applied;
        try
        {
            applied = await _mealPlanTemplateService.ApplyTemplateToWeek(
                id,
                User.GetRequiredUserId(),
                targetWeek,
                IsAdmin());
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Details), new { id });
        }

        if (!applied)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Template applied to your selected week.";
        return RedirectToAction("Weekly", "MealPlans", new { weekStart = targetWeek.ToString("yyyy-MM-dd") });
    }

    /// <summary>
    /// Lists templates waiting for admin review.
    /// </summary>
    [Authorize(Roles = ApplicationRoles.Admin)]
    public async Task<IActionResult> Moderation()
    {
        var templates = await _mealPlanTemplateService.GetPendingTemplates();
        return View(templates.Select(MapToDto).ToList());
    }

    /// <summary>
    /// Approves a submitted template.
    /// </summary>
    [Authorize(Roles = ApplicationRoles.Admin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id)
    {
        var approved = await _mealPlanTemplateService.ApproveTemplate(id);
        if (!approved)
        {
            TempData["ErrorMessage"] = "The template could not be approved. Make sure every referenced recipe is already approved.";
            return RedirectToAction(nameof(Moderation));
        }

        TempData["SuccessMessage"] = "Plan template approved and now visible to everyone.";
        return RedirectToAction(nameof(Moderation));
    }

    /// <summary>
    /// Rejects a submitted template with notes.
    /// </summary>
    [Authorize(Roles = ApplicationRoles.Admin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(ReviewDecisionDto dto)
    {
        var rejected = await _mealPlanTemplateService.RejectTemplate(dto.Id, dto.ReviewNotes);
        if (!rejected)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Plan template rejected with feedback for the owner.";
        return RedirectToAction(nameof(Moderation));
    }

    /// <summary>
    /// Reads the current user id when signed in.
    /// </summary>
    private int? GetCurrentUserId()
    {
        return User.Identity?.IsAuthenticated == true
            ? User.GetRequiredUserId()
            : null;
    }

    /// <summary>
    /// Checks whether the current user is an admin.
    /// </summary>
    private bool IsAdmin()
    {
        return User.IsInRole(ApplicationRoles.Admin);
    }

    /// <summary>
    /// Builds a weekly template DTO for display.
    /// </summary>
    private static MealPlanTemplateDto MapToDto(MealPlanTemplate template)
    {
        var days = Enumerable.Range(0, 7)
            .Select(offset =>
            {
                var meals = template.Meals
                    .Where(meal => meal.DayOffset == offset)
                    .OrderBy(meal => meal.MealType)
                    .Select(meal => new MealPlanTemplateMealDto
                    {
                        RecipeId = meal.RecipeId,
                        MealType = meal.MealType.ToString(),
                        RecipeName = meal.Recipe.Name,
                        Calories = (int)Math.Round(meal.Recipe.Calories * meal.PortionMultiplier, MidpointRounding.AwayFromZero),
                        PortionMultiplier = meal.PortionMultiplier
                    })
                    .ToList();

                return new MealPlanTemplateDayDto
                {
                    Date = template.WeekStart.Date.AddDays(offset),
                    TotalCalories = meals.Sum(m => m.Calories),
                    Meals = meals
                };
            })
            .ToList();

        return new MealPlanTemplateDto
        {
            Id = template.Id,
            Name = template.Name,
            Description = template.Description,
            OwnerId = template.OwnerId,
            OwnerUserName = template.Owner.UserName ?? string.Empty,
            ApprovalStatus = template.ApprovalStatus,
            ReviewNotes = template.ReviewNotes,
            CreatedAt = template.CreatedAt,
            SubmittedAt = template.SubmittedAt,
            ReviewedAt = template.ReviewedAt,
            WeeklyTotalCalories = days.Sum(d => d.TotalCalories),
            Days = days
        };
    }
}
