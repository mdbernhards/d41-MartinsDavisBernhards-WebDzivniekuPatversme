using WebPatversme.Models.Database;

namespace WebPatversme.Models
{
    public class Animals
    {
        private ShelterDatabaseContext context;

        public int AnimalID;

        public string Name;

        public int Age;

        public string BirthDate;//date

        public string Species;

        public string Colour;

        public string About;

        //public string image;

        public int Weight;

        public int DateAdded; //DateTime

        public int FKAnimalSheltersID;
    }
}