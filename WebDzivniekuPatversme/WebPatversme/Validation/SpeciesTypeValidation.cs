using System.Linq;
using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebDzivniekuPatversme.Validation
{
    public class SpeciesTypeValidation : ValidationAttribute
    {
        private readonly bool _successfulIfUnique;

        public SpeciesTypeValidation(bool successfulIfUnique)
        {
            _successfulIfUnique = successfulIfUnique;
        }

        protected override ValidationResult IsValid(object speciesTypeObject, ValidationContext validationContext)
        {
            bool isValid;
            var speciesType = speciesTypeObject as string;
            var animalService = (IAnimalsService)validationContext.GetService(typeof(IAnimalsService));

            if (_successfulIfUnique)
            {
                isValid = !animalService.GetAllSpeciesTypes().Select(x => x.Name).Contains(speciesType);
            }
            else
            {
                isValid = animalService.GetAllSpeciesTypes().Select(x => x.Name).Contains(speciesType);
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(ErrorMessage));
        }
    }
}