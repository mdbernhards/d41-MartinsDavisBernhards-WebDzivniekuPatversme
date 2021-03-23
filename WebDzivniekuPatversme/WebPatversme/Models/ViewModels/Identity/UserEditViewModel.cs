using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class UserEditViewModel
    {
        public ApplicationUser User { get; set; }

        [RoleValidation(false, ErrorMessage = ValidationErrorMessages.DoesNotExistInDb)]
        [Display(Name = "Loma")]
        public string Role { get; set; }

        public List<string> Roles { get; set; }
    }
}