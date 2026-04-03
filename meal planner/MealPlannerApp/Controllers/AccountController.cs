using MealPlannerApp.Dtos.Account;
using MealPlannerApp.Infrastructure;
using MealPlannerApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApp.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(new LoginDto { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var user = await FindUserAsync(dto.Login);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(dto);
        }

        var result = await _signInManager.PasswordSignInAsync(
            user,
            dto.Password,
            dto.RememberMe,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            return RedirectToLocal(dto.ReturnUrl);
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, "This account is temporarily locked. Please try again later.");
            return View(dto);
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(dto);
    }

    [HttpGet]
    public IActionResult Register(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(new RegisterDto { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var user = new User
        {
            UserName = dto.UserName.Trim(),
            Email = dto.Email.Trim(),
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow,
            Role = UserRole.User
        };

        var createResult = await _userManager.CreateAsync(user, dto.Password);
        if (!createResult.Succeeded)
        {
            foreach (var error in createResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(dto);
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(user, ApplicationRoles.User);
        if (!addToRoleResult.Succeeded)
        {
            foreach (var error in addToRoleResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await _userManager.DeleteAsync(user);
            return View(dto);
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToLocal(dto.ReturnUrl);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    private async Task<User?> FindUserAsync(string login)
    {
        var trimmedLogin = login.Trim();
        if (string.IsNullOrWhiteSpace(trimmedLogin))
        {
            return null;
        }

        if (trimmedLogin.Contains('@'))
        {
            return await _userManager.FindByEmailAsync(trimmedLogin)
                ?? await _userManager.FindByNameAsync(trimmedLogin);
        }

        return await _userManager.FindByNameAsync(trimmedLogin)
            ?? await _userManager.FindByEmailAsync(trimmedLogin);
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }

        return RedirectToAction("Weekly", "MealPlans");
    }
}
