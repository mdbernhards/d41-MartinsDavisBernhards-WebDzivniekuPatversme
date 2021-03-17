using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models
{
    public class AnimalSpecies
    {
        public string Id { set; get; }

        [SpeciesValidation(true, ErrorMessage = ValidationErrorMessages.AlreadyExists)]
        [Display(Name = "Suga")]
        public string Name { set; get; }
    }
}