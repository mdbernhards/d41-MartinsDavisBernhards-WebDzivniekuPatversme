using System.Linq;
using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebDzivniekuPatversme.Validation
{
    public class NewsTitleValidation : ValidationAttribute
    {
        private readonly bool _successfulIfUnique;

        public NewsTitleValidation(
            bool successfulIfUnique)
        {
            _successfulIfUnique = successfulIfUnique;
        }

        protected override ValidationResult IsValid(object newsTitleObject, ValidationContext validationContext)
        {
            bool isValid;
            var newsTitle = newsTitleObject as string;
            var animalService = (INewsService)validationContext.GetService(typeof(INewsService));

            if (_successfulIfUnique)
            {
                isValid = !animalService
                    .GetAllNews()
                    .Select(x => x.Title)
                    .Contains(newsTitle);
            }
            else
            {
                isValid = animalService
                    .GetAllNews()
                    .Select(x => x.Title)
                    .Contains(newsTitle);
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}