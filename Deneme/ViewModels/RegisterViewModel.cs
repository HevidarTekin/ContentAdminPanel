using System.ComponentModel.DataAnnotations;

namespace Deneme.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
        [Required]
        public string ConfirmPassword { get; set; } = String.Empty;
        [Required]
        public string SecurityQuestion { get; set; } = string.Empty;
        [Required]
        public string SecurityAnswer { get; set; } = string.Empty;

    }
}
