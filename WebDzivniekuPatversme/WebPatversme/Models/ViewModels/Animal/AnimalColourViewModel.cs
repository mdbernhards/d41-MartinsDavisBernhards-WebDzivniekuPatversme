using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Animal
{
    public class AnimalColourViewModel
    {
        public string Id { set; get; }

        [ColourValidation(true, ErrorMessage = ValidationErrorMessages.AlreadyExists)]
        [Display(Name = "Krāsa")]
        public string Name { set; get; }
    }
}