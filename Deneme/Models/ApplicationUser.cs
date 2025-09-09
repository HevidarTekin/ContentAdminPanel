using Microsoft.AspNetCore.Identity;

namespace Deneme.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string? SecurityQuestion { get; set; }

        public string? SecurityAnswerHash { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
