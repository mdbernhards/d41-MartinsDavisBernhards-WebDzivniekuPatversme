using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models
{
    public class AnimalColour
    {
        public string Id { set; get; }

        [ColourValidation(true, ErrorMessage = ValidationErrorMessages.AlreadyExists)]
        [Display(Name = "Krāsa")]
        public string Name { set; get; }
    }
}