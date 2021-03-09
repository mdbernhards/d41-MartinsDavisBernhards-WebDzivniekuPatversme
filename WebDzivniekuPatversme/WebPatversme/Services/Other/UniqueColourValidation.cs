using System.Linq;
using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebDzivniekuPatversme.Services.Other
{
    public class UniqueColourValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object colourObject, ValidationContext validationContext)
        {
            var colour = colourObject as string;

            var animalService = (IAnimalsService)validationContext.GetService(typeof(IAnimalsService));

            if (!animalService.GetAllColours().Select(x => x.Name).Contains(colour))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(ErrorMessage));
        }
    }
}