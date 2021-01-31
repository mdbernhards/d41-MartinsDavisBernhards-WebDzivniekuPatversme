using System.ComponentModel.DataAnnotations;

namespace WebPatversme.Models
{
    public class Shelters
    {
        public string AnimalShelterID { set; get; }

        public string Name { set; get; }

        public string Address { set; get; }

        public string PhoneNumber { set; get; }

        public int AnimalCapacity { set; get; }

        public string ImagePath { set; get; }
    }
}