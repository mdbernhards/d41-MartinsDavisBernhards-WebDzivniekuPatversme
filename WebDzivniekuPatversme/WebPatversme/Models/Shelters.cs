using System.ComponentModel.DataAnnotations;

namespace WebPatversme.Models
{
    public class Shelters
    {
        [Key]
        public string AnimalShelterID;

        [Required]
        public string Name { set; get; }

        [Required]
        public string Address { set; get; }

        [Required]
        public string PhoneNumber { set; get; }

        [Required]
        public int AnimalCapacity { set; get; }

        public string ImagePath { set; get; }
    }
}