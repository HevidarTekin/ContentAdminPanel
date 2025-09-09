using System.ComponentModel.DataAnnotations;

namespace Deneme.ViewModels
{
    public class ForgotPasswordWithSecurityViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string SecurityAnswer { get; set; } = string.Empty;

    }
}
