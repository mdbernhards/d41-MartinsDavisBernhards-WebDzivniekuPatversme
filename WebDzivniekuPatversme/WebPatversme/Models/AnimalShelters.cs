using WebDzivniekuPatversme.Repository;

namespace WebPatversme.Models
{
    public class AnimalShelters
    {
        public WebShelterDbContext context;

        public int AnimalShelterID;

        public string Name;

        public string Address;

        public string PhoneNumber;

        public int AnimalCapacity;

        public string ImagePath;
    }
}