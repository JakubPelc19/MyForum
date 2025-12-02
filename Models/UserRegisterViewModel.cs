using System.ComponentModel.DataAnnotations;

public class UserRegisterViewModel
{
    
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false)]
    [DataType(DataType.Password)]
    [MaxLength(65, ErrorMessage = "Maximum length is 65")]
    [MinLength(8, ErrorMessage = "Minimum length is 8")]
    [RegularExpression(
        @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one symbol.")]
    public string Password { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false)]
    [DataType(DataType.Password)]
    [MaxLength(65, ErrorMessage = "Maximum length is 65")]
    [MinLength(8, ErrorMessage = "Minimum length is 8")]
    [RegularExpression(
        @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one symbol.")]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}