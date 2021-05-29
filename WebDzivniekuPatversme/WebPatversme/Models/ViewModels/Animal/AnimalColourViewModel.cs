using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Animal
{
    public class AnimalColourViewModel
    {
        public string Id { set; get; }

        [ColourValidation(true, ErrorMessage = ValidationErrorMessages.AlreadyExists)]
        [StringLength(50, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Display(Name = "Krāsa")]
        public string Name { set; get; }
    }
}