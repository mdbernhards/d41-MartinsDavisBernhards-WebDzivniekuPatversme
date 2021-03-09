using WebDzivniekuPatversme.Services.Other;

namespace WebDzivniekuPatversme.Models
{
    public class AnimalSpeciesType
    {
        public string Id { set; get; }

        [UniqueSpeciesTypeValidation(ErrorMessage = "Šī šķirne jau eksistē!")]
        public string Name { set; get; }

        public string SpeciesId { set; get; }
    }
}