using WebDzivniekuPatversme.Validation;

namespace WebDzivniekuPatversme.Models
{
    public class AnimalSpeciesType
    {
        public string Id { set; get; }

        [SpeciesTypeValidation(true, ErrorMessage = "Šī šķirne jau eksistē!")]
        public string Name { set; get; }

        public string SpeciesId { set; get; }
    }
}