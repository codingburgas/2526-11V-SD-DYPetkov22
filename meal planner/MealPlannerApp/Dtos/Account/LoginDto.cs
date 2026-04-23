using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Dtos.Account;

/// <summary>
/// Login form values.
/// </summary>
public class LoginDto
{
    /// <summary>Email address or username.</summary>
    [Required]
    [Display(Name = "Email or Username")]
    public string Login { get; set; } = string.Empty;

    /// <summary>User password.</summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    /// <summary>True when the browser should stay signed in.</summary>
    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }

    /// <summary>Safe local URL to return to after login.</summary>
    public string? ReturnUrl { get; set; }
}
