using WebDzivniekuPatversme.Services.Other;

namespace WebDzivniekuPatversme.Models
{
    public class AnimalColour
    {
        public string Id { set; get; }

        [UniqueColourValidation(ErrorMessage = "Šī krāsa jau eksistē!")]
        public string Name { set; get; }
    }
}