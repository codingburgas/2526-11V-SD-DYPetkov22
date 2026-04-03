using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.Dtos.Account;

public class LoginDto
{
    [Required]
    [Display(Name = "Email or Username")]
    public string Login { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}
