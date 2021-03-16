using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class DeletePersonalDataViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredF)]
        [Display(Name = "Parole")]
        public string Password { get; set; }
    }
}