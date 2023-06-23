namespace mtg.Models;

using System.ComponentModel.DataAnnotations;

public class RegistrationViewModel
{
    [Required]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "The email address must be between 5 and 20 characters.")]
    public string Email { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 20 characters.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[^A-Za-z0-9\s]).+$", 
        ErrorMessage = "The password must contain an upper case letter and a special character.")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords must match.")]
    public string RepeatedPassword { get; set; }
    public bool KeepLoggedIn { get; set; }
}