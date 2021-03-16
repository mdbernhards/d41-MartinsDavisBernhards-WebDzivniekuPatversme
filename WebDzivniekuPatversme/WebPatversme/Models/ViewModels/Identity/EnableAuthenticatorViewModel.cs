using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class EnableAuthenticatorViewModel
    {
        [DataType(DataType.Text)]
        [StringLength(7, MinimumLength = 6, ErrorMessage = ValidationErrorMessages.VerificationCodeStringLenght)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Verifikācijas kods")]
        public string Code { get; set; }
    }
}