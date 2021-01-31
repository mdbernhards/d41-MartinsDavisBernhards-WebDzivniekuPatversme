using System.ComponentModel.DataAnnotations;

namespace WebPatversme.Models.ViewModels
{
    public class SheltersViewModel
    {
        [Required]
        public string Name { set; get; }

        [Required]
        public string Address { set; get; }

        [Required]
        public string PhoneNumber { set; get; }

        [Required]
        public int AnimalCapacity { set; get; }

        [Required]
        public string ImagePath { set; get; }
    }
}