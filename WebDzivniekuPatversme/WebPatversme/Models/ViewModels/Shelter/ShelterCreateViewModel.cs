using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Shelters
{
    public class ShelterCreateViewModel
    {
        [StringLength(255, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Nosaukums")]
        public string Name { set; get; }

        [EmailAddress(ErrorMessage = ValidationErrorMessages.NotValid)]
        [StringLength(255, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "E-pasts")]
        public string Email { set; get; }

        [StringLength(19000, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Display(Name = "Apraksts")]
        public string Description { set; get; }

        [StringLength(255, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredF)]
        [Display(Name = "Adrese")]
        public string Address { set; get; }

        [Phone(ErrorMessage = ValidationErrorMessages.NotValid)]
        [StringLength(25, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Telefona numurs")]
        public string PhoneNumber { set; get; }

        [Range(1, 9999, ErrorMessage = ValidationErrorMessages.CapacityRange)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredF)]
        [Display(Name = "Dzīvnieku kapacitāte")]
        public int AnimalCapacity { set; get; }

        [DataType(DataType.Upload)]
        [MaxFileSizeValidation(6 * 1024 * 1024, ErrorMessage = ValidationErrorMessages.MaxFileSize)]
        [ExtensionValidation(new string[] { ".jpg", ".png", ".jpeg", ".gif", ".tif" }, ErrorMessage = ValidationErrorMessages.FileType)]
        [Display(Name = "Attēls")]
        public IFormFile Image { set; get; }
    }
}