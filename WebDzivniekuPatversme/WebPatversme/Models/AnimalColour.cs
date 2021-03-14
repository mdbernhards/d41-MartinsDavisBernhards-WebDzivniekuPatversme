using WebDzivniekuPatversme.Validation;

namespace WebDzivniekuPatversme.Models
{
    public class AnimalColour
    {
        public string Id { set; get; }

        [ColourValidation(true, ErrorMessage = "Šī krāsa jau eksistē!")]
        public string Name { set; get; }
    }
}