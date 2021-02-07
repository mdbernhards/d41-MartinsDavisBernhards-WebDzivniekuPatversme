using System;
using System.ComponentModel.DataAnnotations;

namespace WebDzivniekuPatversme.Models.ViewModels
{
    public class SheltersViewModel
    {
        [Key]
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

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        public string ImagePath { set; get; }
    }
}