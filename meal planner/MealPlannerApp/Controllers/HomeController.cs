using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MealPlannerApp.Models;

namespace MealPlannerApp.Controllers;

/// <summary>
/// Serves the public home, privacy, and error pages.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Shows the landing page.
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Shows the privacy page.
    /// </summary>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Shows request error details.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
