using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Animal
{
    public class AnimalSpeciesViewModel
    {
        public string Id { set; get; }

        [SpeciesValidation(true, ErrorMessage = ValidationErrorMessages.AlreadyExists)]
        [StringLength(50, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Display(Name = "Suga")]
        public string Name { set; get; }
    }
}