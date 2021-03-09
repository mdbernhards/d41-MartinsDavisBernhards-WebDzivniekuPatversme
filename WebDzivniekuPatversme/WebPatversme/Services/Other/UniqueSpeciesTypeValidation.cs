using System.Linq;
using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebDzivniekuPatversme.Services.Other
{
    public class UniqueSpeciesTypeValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object speciesTypeObject, ValidationContext validationContext)
        {
            var speciesType = speciesTypeObject as string;

            var animalService = (IAnimalsService)validationContext.GetService(typeof(IAnimalsService));

            if (!animalService.GetAllSpeciesTypes().Select(x => x.Name).Contains(speciesType))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(ErrorMessage));
        }
    }
}