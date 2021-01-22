using WebPatversme.Models.Database;

namespace WebPatversme.Models
{
    public class AnimalShelters
    {
        private ShelterDatabaseContext context;

        public int AnimalShelterID;

        public string Name;

        public string Address;

        public string PhoneNumber;

        public int AnimalCapacity;

        //public int image; ??
    }
}