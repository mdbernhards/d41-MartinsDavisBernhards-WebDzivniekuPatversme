using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Animal
{
    public class AnimalCreateViewModel
    {
        [StringLength(50, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Vārds")]
        public string Name { set; get; }

        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = ValidationErrorMessages.DateValidation)]
        [Display(Name = "Dzimšanas datums")]
        public DateTime BirthDate { set; get; }

        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = ValidationErrorMessages.DateValidation)]
        [Display(Name = "Dzimšanas datums līdz")]
        public DateTime BirthDateRangeTo { set; get; }

        [GenderValidation(ErrorMessage = ValidationErrorMessages.DoesNotExistInDb)]
        [StringLength(20, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Dzimums")]
        public string Gender { set; get; }

        [SpeciesValidation(false, ErrorMessage = ValidationErrorMessages.DoesNotExistInDb)]
        [StringLength(100, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredF)]
        [Display(Name = "Suga")]
        public string Species { set; get; }

        [SpeciesTypeValidation(false, ErrorMessage = ValidationErrorMessages.DoesNotExistInDb)]
        [StringLength(100, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Display(Name = "Šķirne")]
        public string SpeciesType { set; get; }

        [ColourValidation(false, ErrorMessage = ValidationErrorMessages.DoesNotExistInDb)]
        [StringLength(100, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredF)]
        [Display(Name = "Krāsa")]
        public string Colour { set; get; }

        [ColourValidation(false, ErrorMessage = ValidationErrorMessages.DoesNotExistInDb)]
        [StringLength(100, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Display(Name = "Sekundārā krāsa")]
        public string SecondaryColour { set; get; }

        [StringLength(5000, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Display(Name = "Apraksts")]
        public string About { set; get; }

        [DataType(DataType.Upload)]
        [MaxFileSizeValidation(6 * 1024 * 1024, ErrorMessage = ValidationErrorMessages.MaxFileSize)]
        [ExtensionValidation(new string[] { ".jpg", ".png", ".jpeg", ".gif", ".tif" }, ErrorMessage = ValidationErrorMessages.FileType)]
        [Display(Name = "Attēls")]
        public IFormFile Image { set; get; }

        [Range(0.01, 250, ErrorMessage = ValidationErrorMessages.WeightRange)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Svars (Kg)")]
        public double Weight { set; get; }

        [ShelterValidation(ErrorMessage = ValidationErrorMessages.DoesNotExistInDb)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredF)]
        [Display(Name = "Patversme")]
        public string ShelterId { set; get; }

        [Display(Name = "Patversme")]
        public string ShelterName {set; get; }

        public IEnumerable<Shelter> AnimalShelters { get; set; }

        public IEnumerable<AnimalColourViewModel> AnimalColours { get; set; }

        public IEnumerable<AnimalSpeciesViewModel> AnimalSpecies { get; set; }

        public IEnumerable<AnimalSpeciesTypeViewModel> AnimalSpeciesTypes { get; set; }
    }
}