using System.Linq;
using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebDzivniekuPatversme.Validation
{
    public class ColourValidation : ValidationAttribute
    {
        private readonly bool _successfulIfUnique;

        public ColourValidation(bool successfulIfUnique)
        {
            _successfulIfUnique = successfulIfUnique;
        }

        protected override ValidationResult IsValid(object colourObject, ValidationContext validationContext)
        {
            bool isValid;
            var colour = colourObject as string;
            var animalService = (IAnimalsService)validationContext.GetService(typeof(IAnimalsService));

            if (_successfulIfUnique)
            {
                isValid = !animalService.GetAllColours().Select(x => x.Name).Contains(colour);
            }
            else
            {
                isValid = animalService.GetAllColours().Select(x => x.Name).Contains(colour);
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(ErrorMessage));
        }
    }
}