using System;
using System.ComponentModel.DataAnnotations;

namespace WebDzivniekuPatversme.Models.ViewModels.Animal
{
    public class AnimalDetailsViewModel
    {
        [Key]
        public string Id { set; get; }

        [Display(Name = "Vārds")]
        public string Name { set; get; }

        [Display(Name = "Vecums")]
        public string Age { set; get; }

        [DataType(DataType.Date)]
        [Display(Name = "Dzimšanas datums")]
        public DateTime BirthDate { set; get; }

        [Display(Name = "Dzimums")]
        public string Gender { set; get; }

        [Display(Name = "Suga")]
        public string Species { set; get; }

        [Display(Name = "Šķirne")]
        public string SpeciesType { set; get; }

        [Display(Name = "Krāsa")]
        public string Colour { set; get; }

        [Display(Name = "Sekundārā krāsa")]
        public string SecondaryColour { set; get; }

        [Display(Name = "Apraksts")]
        public string About { set; get; }

        public string ImagePath { set; get; }

        [Display(Name = "Svars (Kg)")]
        public double Weight { set; get; }

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        public string ShelterId { set; get; }

        [Display(Name = "Patversme")]
        public string ShelterName {set; get; }

        [Display(Name = "E-pasts")]
        public string EmailMessage { get; set; }

        [Display(Name = "E-pasta tituls")]
        public string EmailTitle { get; set; }
    }
}