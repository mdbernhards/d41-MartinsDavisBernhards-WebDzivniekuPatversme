using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebDzivniekuPatversme.Validation
{
    public class MaxFileSizeValidation : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeValidation(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
            object value, 
            ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maksimālais faila lielums ir 6 MB.";
        }
    }
}