using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class IndexViewModel
    {
        [StringLength(50, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Display(Name = "Vārds")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Display(Name = "Uzvārds")]
        public string Surname { get; set; }

        [Phone(ErrorMessage = ValidationErrorMessages.NotValid)]
        [Display(Name = "Telefona numurs")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSizeValidation(6 * 1024 * 1024, ErrorMessage = ValidationErrorMessages.MaxFileSize)]
        [ExtensionValidation(new string[] { ".jpg", ".png", ".jpeg", ".gif", ".tif" }, ErrorMessage = ValidationErrorMessages.FileType)]
        [Display(Name = "Profila attēls")]
        public IFormFile Image { set; get; }

        public string ImageString { set; get; }
    }
}