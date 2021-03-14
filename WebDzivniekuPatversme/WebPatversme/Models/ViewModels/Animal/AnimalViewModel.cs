using System;
using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Validation;

namespace WebDzivniekuPatversme.Models.ViewModels.Animal
{
    public class AnimalViewModel
    {
        [Key]
        public string Id { set; get; }

        [StringLength(100, ErrorMessage = "Vārds par garu!")]
        [Required(ErrorMessage = "Vārds ir obligāts!")]
        [Display(Name = "Vārds")]
        public string Name { set; get; }

        [Display(Name = "Vecums")]
        public string Age { set; get; }

        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Datums nevar būt nākotnē!")]
        [Display(Name = "Dzimšanas datums")]
        public DateTime BirthDate { set; get; }

        [StringLength(100, ErrorMessage = "Sugas nosaukums par garu!")]
        [SpeciesValidation(false, ErrorMessage = "Šī suga datubāzē neeksistē!")]
        [Required(ErrorMessage = "Suga ir obligāta!")]
        [Display(Name = "Suga")]
        public string Species { set; get; }

        [StringLength(100, ErrorMessage = "Šķirnes nosaukums par garu!")]
        [SpeciesTypeValidation(false, ErrorMessage = "Šī šķirne datubāzē neeksistē!")]
        [Display(Name = "Šķirne")]
        public string SpeciesType { set; get; }

        [StringLength(100, ErrorMessage = "Krāsas nosaukums par garu!")]
        [ColourValidation(false, ErrorMessage = "Šī krāsa datubāzē neeksistē!")]
        [Required(ErrorMessage = "Krāsa ir obligāta!")]
        [Display(Name = "Krāsa")]
        public string Colour { set; get; }

        [StringLength(100, ErrorMessage = "Sekundārās krāsas nosaukums par garu!")]
        [ColourValidation(false, ErrorMessage = "Šī krāsa datubāzē neeksistē!")]
        [Display(Name = "Sekundārā krāsa")]
        public string SecondaryColour { set; get; }

        public string ImagePath { set; get; }

        [Required(ErrorMessage = "Svars ir obligāts!")]
        [Range(0.01, 250, ErrorMessage = "Svars Jābūt lielākam par 0 un mazākam par 250 Kg!")]
        [Display(Name = "Svars (Kg)")]
        public double Weight { set; get; }

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        [ShelterValidation(ErrorMessage = "Šī patversme neeksistē!")]
        [Required(ErrorMessage = "Patversme ir obligāta!")]
        [Display(Name = "Patversme")]
        public string ShelterId { set; get; }

        [Display(Name = "Patversme")]
        public string ShelterName {set; get; }
    }
}