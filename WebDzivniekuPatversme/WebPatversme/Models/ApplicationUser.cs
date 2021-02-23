using Microsoft.AspNetCore.Identity;

namespace WebDzivniekuPatversme.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string ImagePath { get; set; }
    }
}