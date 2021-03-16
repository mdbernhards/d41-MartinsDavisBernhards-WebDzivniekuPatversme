using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class LoginWith2faViewModel
    {
        [DataType(DataType.Text)]
        [StringLength(7, MinimumLength = 6, ErrorMessage = ValidationErrorMessages.VerificationCodeStringLenght)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Verifikācijas kods")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Atcerēties šo ierīci")]
        public bool RememberMachine { get; set; }
    }
}