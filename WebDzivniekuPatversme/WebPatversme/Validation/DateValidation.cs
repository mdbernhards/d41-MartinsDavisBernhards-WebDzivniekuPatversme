using System;
using System.ComponentModel.DataAnnotations;

namespace WebDzivniekuPatversme.Validation
{
    public class DateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object dateObject, ValidationContext validationContext)
        {
            var dateValue = dateObject as DateTime? ?? new DateTime();

            if (dateValue.Date > DateTime.Now.Date)
            {
                return new ValidationResult(FormatErrorMessage(ErrorMessage));
            }

            return ValidationResult.Success;
        }
    }
}