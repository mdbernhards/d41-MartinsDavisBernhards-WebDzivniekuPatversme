using System.Linq;
using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebDzivniekuPatversme.Services.Other
{
    public class ShelterValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object shelterIdObject, ValidationContext validationContext)
        {
            var shelterId = shelterIdObject as string;

            var shelter = (IShelterService)validationContext.GetService(typeof(IShelterService));

            if (shelter.GetAllShelterList().Select(x => x.AnimalShelterID).Contains(shelterId))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(ErrorMessage));
        }
    }
}