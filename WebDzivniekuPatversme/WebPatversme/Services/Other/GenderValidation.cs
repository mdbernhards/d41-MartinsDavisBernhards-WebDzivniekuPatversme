using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Services.Other
{
    public class GenderValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object genderObject, ValidationContext validationContext)
        {
            var genderValue = genderObject as string;

            if (genderValue == Gender.Male || genderValue == Gender.Female)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(ErrorMessage));
        }
    }
}