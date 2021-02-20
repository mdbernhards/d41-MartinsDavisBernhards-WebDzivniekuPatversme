using System;
using System.IO;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebDzivniekuPatversme.Services.Other
{
    public class ExtensionValidation : ValidationAttribute
    {
        private readonly string[] _extensions;
        public ExtensionValidation(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(
            object value, 
            ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Šis faila tips nav atļauts!";
        }
    }
}