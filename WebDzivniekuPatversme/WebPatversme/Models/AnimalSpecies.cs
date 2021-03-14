using WebDzivniekuPatversme.Validation;

namespace WebDzivniekuPatversme.Models
{
    public class AnimalSpecies
    {
        public string Id { set; get; }

        [SpeciesValidation(true, ErrorMessage = "Šī suga jau eksistē!")]
        public string Name { set; get; }
    }
}