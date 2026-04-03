using MealPlannerApp.Dtos.Moderation;
using MealPlannerApp.Dtos.Recipes;
using MealPlannerApp.Infrastructure;
using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApp.Controllers;

public class RecipesController : Controller
{
    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    public async Task<IActionResult> Index(string? ingredientName, bool vegetarianOnly = false, bool highProteinOnly = false)
    {
        var recipes = await _recipeService.GetAllRecipes(
            GetCurrentUserId(),
            IsAdmin(),
            ingredientName,
            vegetarianOnly,
            highProteinOnly);

        var dto = recipes.Select(MapToDto).ToList();
        ViewData["IngredientName"] = ingredientName;
        ViewData["VegetarianOnly"] = vegetarianOnly;
        ViewData["HighProteinOnly"] = highProteinOnly;
        return View(dto);
    }

    public async Task<IActionResult> Details(int id)
    {
        var recipe = await _recipeService.GetRecipeById(id, GetCurrentUserId(), IsAdmin());
        if (recipe is null)
        {
            return NotFound();
        }

        return View(MapToDto(recipe));
    }

    [Authorize]
    public IActionResult Create()
    {
        return View(new RecipeDto());
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RecipeDto dto, bool submitForReview = false)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await _recipeService.CreateRecipe(User.GetRequiredUserId(), MapToEntity(dto), submitForReview);
        TempData["SuccessMessage"] = submitForReview
            ? "Recipe submitted for admin review."
            : "Recipe saved as a private draft.";
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var recipe = await _recipeService.GetRecipeById(id, GetCurrentUserId(), IsAdmin());
        if (recipe is null)
        {
            return NotFound();
        }

        if (!CanManage(recipe))
        {
            return Forbid();
        }

        if (!IsAdmin() && recipe.ApprovalStatus == ApprovalStatus.Approved)
        {
            TempData["ErrorMessage"] = "Approved recipes are read-only. Create a new draft instead.";
            return RedirectToAction(nameof(Details), new { id });
        }

        return View(MapToDto(recipe));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RecipeDto dto, bool submitForReview = false)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var updated = await _recipeService.UpdateRecipe(id, User.GetRequiredUserId(), IsAdmin(), MapToEntity(dto), submitForReview);
        if (!updated)
        {
            return Forbid();
        }

        TempData["SuccessMessage"] = submitForReview
            ? "Recipe updated and submitted for admin review."
            : "Recipe changes saved.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var recipe = await _recipeService.GetRecipeById(id, GetCurrentUserId(), IsAdmin());
        if (recipe is null)
        {
            return NotFound();
        }

        if (!CanManage(recipe))
        {
            return Forbid();
        }

        return View(MapToDto(recipe));
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _recipeService.DeleteRecipe(id, User.GetRequiredUserId(), IsAdmin());
        if (deleted == DeleteOperationResult.NotFound)
        {
            return NotFound();
        }

        if (deleted == DeleteOperationResult.Forbidden)
        {
            return Forbid();
        }

        if (deleted == DeleteOperationResult.InUse)
        {
            TempData["ErrorMessage"] = "This recipe is already used in meal plans or shared templates and cannot be deleted.";
            return RedirectToAction(nameof(Details), new { id });
        }

        TempData["SuccessMessage"] = "Recipe removed.";
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitForReview(int id)
    {
        var submitted = await _recipeService.SubmitForReview(id, User.GetRequiredUserId(), IsAdmin());
        if (!submitted)
        {
            return Forbid();
        }

        TempData["SuccessMessage"] = "Recipe submitted for admin review.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [Authorize(Roles = ApplicationRoles.Admin)]
    public async Task<IActionResult> Moderation()
    {
        var recipes = await _recipeService.GetPendingRecipes();
        return View(recipes.Select(MapToDto).ToList());
    }

    [Authorize(Roles = ApplicationRoles.Admin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id)
    {
        var approved = await _recipeService.ApproveRecipe(id);
        if (!approved)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Recipe approved and now visible to everyone.";
        return RedirectToAction(nameof(Moderation));
    }

    [Authorize(Roles = ApplicationRoles.Admin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(ReviewDecisionDto dto)
    {
        var rejected = await _recipeService.RejectRecipe(dto.Id, dto.ReviewNotes);
        if (!rejected)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Recipe rejected with feedback for the owner.";
        return RedirectToAction(nameof(Moderation));
    }

    private int? GetCurrentUserId()
    {
        return User.Identity?.IsAuthenticated == true
            ? User.GetRequiredUserId()
            : null;
    }

    private bool IsAdmin()
    {
        return User.IsInRole(ApplicationRoles.Admin);
    }

    private bool CanManage(Recipe recipe)
    {
        return IsAdmin() || recipe.OwnerId == GetCurrentUserId();
    }

    private static RecipeDto MapToDto(Recipe recipe)
    {
        return new RecipeDto
        {
            Id = recipe.Id,
            OwnerId = recipe.OwnerId,
            OwnerUserName = recipe.Owner.UserName ?? string.Empty,
            ApprovalStatus = recipe.ApprovalStatus,
            ReviewNotes = recipe.ReviewNotes,
            Name = recipe.Name,
            Description = recipe.Description,
            Instructions = recipe.Instructions,
            CookingTime = recipe.CookingTime,
            Calories = recipe.Calories,
            IsVegetarian = recipe.IsVegetarian,
            Ingredients = recipe.RecipeIngredients
                .OrderBy(ri => ri.Ingredient.Name)
                .Select(ri => new RecipeIngredientDto
                {
                    Name = ri.Ingredient.Name,
                    QuantityInGrams = ri.QuantityInGrams
                })
                .ToList()
        };
    }

    private static Recipe MapToEntity(RecipeDto dto)
    {
        return new Recipe
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Instructions = dto.Instructions,
            CookingTime = dto.CookingTime,
            Calories = dto.Calories,
            IsVegetarian = dto.IsVegetarian
        };
    }
}
