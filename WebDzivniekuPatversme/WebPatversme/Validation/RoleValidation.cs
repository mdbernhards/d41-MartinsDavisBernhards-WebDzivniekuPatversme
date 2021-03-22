using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebDzivniekuPatversme.Validation
{
    public class RoleValidation : ValidationAttribute
    {
        private readonly bool _successfulIfUnique;

        public RoleValidation(bool successfulIfUnique)
        {
            _successfulIfUnique = successfulIfUnique;
        }

        protected override ValidationResult IsValid(object roleObject, ValidationContext validationContext)
        {
            bool isValid;
            var role = roleObject as string;
            var roleManager = (RoleManager<IdentityRole>)validationContext.GetService(typeof(RoleManager<IdentityRole>));

            if (_successfulIfUnique)
            {
                isValid = !roleManager.Roles
                    .ToList()
                    .Select(x => x.Name)
                    .Contains(role);
            }
            else
            {
                isValid = roleManager.Roles
                    .ToList()
                    .Select(x => x.Name)
                    .Contains(role);
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(ErrorMessage));
        }
    }
}