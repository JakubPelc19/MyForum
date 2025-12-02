using System.ComponentModel.DataAnnotations;

public class UserLoginViewModel
{
    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;
}