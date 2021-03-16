using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = ValidationErrorMessages.NotValid)]
        [StringLength(255, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "E-pasts")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredF)]
        [Display(Name = "Parole")]
        public string Password { get; set; }

        [Display(Name = "Atcerēties mani?")]
        public bool RememberMe { get; set; }
    }
}