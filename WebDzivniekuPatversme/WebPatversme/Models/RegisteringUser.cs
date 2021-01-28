using Microsoft.AspNetCore.Identity;

namespace WebDzivniekuPatversme.Models
{
    public class RegisteringUser : IdentityUser
    {
        public string Email { get; set; }

        public bool ConfirmedEmail { get; set; }
    }
}
