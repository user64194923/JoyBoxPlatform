using Microsoft.AspNetCore.Identity;

namespace JoyBoxPlatform.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nickname { get; set; } = string.Empty;
    }
}
