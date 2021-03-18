using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Animal
{
    public class AnimalSpeciesTypeViewModel
    {
        public string Id { set; get; }

        [SpeciesTypeValidation(true, ErrorMessage = ValidationErrorMessages.AlreadyExists)]
        [Display(Name = "Šķirne")]
        public string Name { set; get; }

        public string SpeciesId { set; get; }
    }
}