namespace mtg.Models;

using System.ComponentModel.DataAnnotations;

public class AuthViewModel
{
    public string? Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "The email address must be between 5 and 20 characters.")]
    public string Email { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 20 characters.")]
    [RegularExpression(@"^[^\s]+$")]
    public string Password { get; set; }
    public bool KeepLoggedIn { get; set; }
}