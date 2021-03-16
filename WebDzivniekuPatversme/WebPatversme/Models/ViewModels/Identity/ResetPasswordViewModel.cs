using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class ResetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = ValidationErrorMessages.NotValid)]
        [StringLength(255, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "E-pasts")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = ValidationErrorMessages.NewPasswordStringLenght)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredF)]
        [Display(Name = "Parole")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = ValidationErrorMessages.ComparePassword)]
        [Display(Name = "Apstiprini paroli")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}