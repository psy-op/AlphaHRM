using Microsoft.AspNetCore.Identity;

namespace AlphaHRM.Models
{
    public class AppUser : IdentityUser { }
    public class LoginRequest 
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

}
