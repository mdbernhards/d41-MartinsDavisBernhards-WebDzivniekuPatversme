using System;
using System.ComponentModel.DataAnnotations;

namespace WebDzivniekuPatversme.Models.ViewModels.Shelters
{
    public class ShelterViewModel
    {
        [Key]
        public string Id { set; get; }

        [Display(Name = "Nosaukums")]
        public string Name { set; get; }

        [Display(Name = "Adrese")]
        public string Address { set; get; }

        [Display(Name = "Telefona numurs")]
        public string PhoneNumber { set; get; }

        [Display(Name = "Dzīvnieku kapacitāte")]
        public int AnimalCapacity { set; get; }

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        public string ImagePath { set; get; }
    }
}