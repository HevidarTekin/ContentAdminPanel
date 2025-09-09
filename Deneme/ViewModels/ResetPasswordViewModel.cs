using System.ComponentModel.DataAnnotations;

namespace Deneme.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = String.Empty;

        [Required]
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
