using System.ComponentModel.DataAnnotations;

namespace WebPatversme.Models.ViewModels
{
    public class SheltersViewModel
    {
        public string AnimalShelterID { set; get; }

        [Required(ErrorMessage = "Vārds ir obligāts.")]
        [Display(Name = "Vārds")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Adrese ir obligāta.")]
        [Display(Name = "Adrese")]
        public string Address { set; get; }

        [Required(ErrorMessage = "Telefona numurs ir obligāts.")]
        [Display(Name = "Telefona numurs")]
        public string PhoneNumber { set; get; }

        [Required(ErrorMessage = "Dzīvnieku kapacitāte ir obligāta.")]
        [Display(Name = "Dzīvnieku kapacitāte")]
        public int AnimalCapacity { set; get; }

        public string ImagePath { set; get; }
    }
}