﻿using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class RoleControlViewModel
    {
        public ApplicationUser User { get; set; }

        [RoleValidation(false, ErrorMessage = ValidationErrorMessages.DoesNotExistInDb)]
        [Display(Name = "Loma")]
        public string Role { get; set; }
    }
}