using System;
using System.ComponentModel.DataAnnotations;

namespace WebDzivniekuPatversme.Models.ViewModels.Shelters
{
    public class ShelterDetailsViewModel
    {
        [Key]
        public string Id { set; get; }

        [StringLength(100, ErrorMessage = "Patversmes nosaukums par garu")]
        [Required(ErrorMessage = "Vārds ir obligāts.")]
        [Display(Name = "Nosaukums")]
        public string Name { set; get; }

        [Required(ErrorMessage = "E-pasts ir obligāts.")]
        [EmailAddress]
        [Display(Name = "E-pasts")]
        public string Email { set; get; }

        [StringLength(19000, ErrorMessage = "Apraksts par garu")]
        [Display(Name = "Apraksts")]
        public string Description { set; get; }

        [Required(ErrorMessage = "Adrese ir obligāta.")]
        [Display(Name = "Adrese")]
        public string Address { set; get; }

        [StringLength(25, ErrorMessage = "Telefona numurs par garu")]
        [Phone(ErrorMessage = "Telefona numurs ievadīts nepareizi")]
        [Required(ErrorMessage = "Telefona numurs ir obligāts.")]
        [Display(Name = "Telefona numurs")]
        public string PhoneNumber { set; get; }

        [Range(1, 9999, ErrorMessage = "Kapacitāte nevar būt mazāka par 1 un lielāka par 9999.")]
        [Required(ErrorMessage = "Dzīvnieku kapacitāte ir obligāta.")]
        [Display(Name = "Dzīvnieku kapacitāte")]
        public int AnimalCapacity { set; get; }

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        public string ImagePath { set; get; }
    }
}