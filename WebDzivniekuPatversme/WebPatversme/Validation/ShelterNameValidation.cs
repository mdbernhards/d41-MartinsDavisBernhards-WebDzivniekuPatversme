using System.Linq;
using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebDzivniekuPatversme.Validation
{
    public class ShelterNameValidation : ValidationAttribute
    {
        private readonly bool _successfulIfUnique;

        public ShelterNameValidation(bool successfulIfUnique)
        {
            _successfulIfUnique = successfulIfUnique;
        }

        protected override ValidationResult IsValid(object shelterNameObject, ValidationContext validationContext)
        {
            bool isValid;
            var shelterName = shelterNameObject as string;
            var shelterService = (IShelterService)validationContext.GetService(typeof(IShelterService));

            if (_successfulIfUnique)
            {
                isValid = !shelterService.GetAllShelters()
                    .Select(x => x.Name)
                    .Contains(shelterName);
            }
            else
            {
                isValid = shelterService.GetAllShelters()
                    .Select(x => x.Name)
                    .Contains(shelterName);
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}