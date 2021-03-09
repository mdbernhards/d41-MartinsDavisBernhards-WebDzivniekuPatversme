using WebDzivniekuPatversme.Services.Other;

namespace WebDzivniekuPatversme.Models
{
    public class AnimalSpecies
    {
        public string Id { set; get; }

        [UniqueSpeciesValidation(ErrorMessage = "Šī suga jau eksistē!")]
        public string Name { set; get; }
    }
}