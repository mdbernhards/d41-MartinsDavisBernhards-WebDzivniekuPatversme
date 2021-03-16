using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class LoginWithRecoveryCodeViewModel
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

        [BindProperty]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Atjaunošanas kods")]
        public string RecoveryCode { get; set; }
    }
}