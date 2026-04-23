using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Dtos.Account;

/// <summary>
/// Registration form values.
/// </summary>
public class RegisterDto
{
    /// <summary>Requested username.</summary>
    [Required]
    [Display(Name = "Username")]
    [StringLength(30, MinimumLength = 3)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>Requested email address.</summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>Requested password.</summary>
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [StringLength(100, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).+$",
        ErrorMessage = "Password must include uppercase, lowercase, number, and symbol.")]
    public string Password { get; set; } = string.Empty;

    /// <summary>Password confirmation.</summary>
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;

    /// <summary>Safe local URL to return to after registration.</summary>
    public string? ReturnUrl { get; set; }
}
