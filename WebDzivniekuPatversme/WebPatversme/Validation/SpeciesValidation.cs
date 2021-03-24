using System.Linq;
using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebDzivniekuPatversme.Validation
{
    public class SpeciesValidation : ValidationAttribute
    {
        private readonly bool _successfulIfUnique;

        public SpeciesValidation(
            bool successfulIfUnique)
        {
            _successfulIfUnique = successfulIfUnique;
        }

        protected override ValidationResult IsValid(object speciesObject, ValidationContext validationContext)
        {
            bool isValid;
            var species = speciesObject as string;
            var animalService = (IAnimalsService)validationContext.GetService(typeof(IAnimalsService));

            if (_successfulIfUnique)
            {
                isValid = !animalService.GetAllSpecies()
                    .Select(x => x.Name)
                    .Contains(species);
            }
            else
            {
                isValid = animalService.GetAllSpecies()
                    .Select(x => x.Name)
                    .Contains(species);
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}