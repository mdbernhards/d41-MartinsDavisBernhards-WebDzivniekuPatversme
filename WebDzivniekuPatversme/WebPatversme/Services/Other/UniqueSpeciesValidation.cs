using System.Linq;
using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebDzivniekuPatversme.Services.Other
{
    public class UniqueSpeciesValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object speciesObject, ValidationContext validationContext)
        {
            var species = speciesObject as string;

            var animalService = (IAnimalsService)validationContext.GetService(typeof(IAnimalsService));

            if (!animalService.GetAllSpecies().Select(x => x.Name).Contains(species))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(ErrorMessage));
        }
    }
}