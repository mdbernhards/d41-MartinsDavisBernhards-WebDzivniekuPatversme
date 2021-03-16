using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class EmailViewModel
    {
        [EmailAddress(ErrorMessage = ValidationErrorMessages.NotValid)]
        [StringLength(255, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Jaunais E-pasts")]
        public string NewEmail { get; set; }
    }
}