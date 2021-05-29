using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Display(Name = "Lietotājvārds")]
        public string UserName { get; set; }

        [StringLength(50, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Vārds")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Uzvārds")]
        public string Surname { get; set; }

        [EmailAddress(ErrorMessage = ValidationErrorMessages.NotValid)]
        [StringLength(255, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "E-pasts")]
        public string Email { get; set; }

        [Phone(ErrorMessage = ValidationErrorMessages.NotValid)]
        [StringLength(25, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Display(Name = "Telefona numurs")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSizeValidation(6 * 1024 * 1024, ErrorMessage = ValidationErrorMessages.MaxFileSize)]
        [ExtensionValidation(new string[] { ".jpg", ".png", ".jpeg", ".gif", ".tif" }, ErrorMessage = ValidationErrorMessages.FileType)]
        [Display(Name = "Profila attēls")]
        public IFormFile Image { set; get; }

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = ValidationErrorMessages.NewPasswordStringLenght)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredF)]
        [Display(Name = "Parole")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = ValidationErrorMessages.ComparePassword)]
        [Display(Name = "Apstiprini paroli")]
        public string ConfirmPassword { get; set; }
    }
}