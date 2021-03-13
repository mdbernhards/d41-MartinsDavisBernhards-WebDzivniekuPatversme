using System;
using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Services.Other;

namespace WebDzivniekuPatversme.Models.ViewModels.Animal
{
    public class AnimalDetailsViewModel
    {
        [Key]
        public string Id { set; get; }

        [StringLength(100, ErrorMessage = "Vārds par garu")]
        [Required(ErrorMessage = "Vārds ir obligāts.")]
        [Display(Name = "Vārds")]
        public string Name { set; get; }

        [Display(Name = "Vecums")]
        public string Age { set; get; }

        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Datums nevar būt nākotnē")]
        [Display(Name = "Dzimšanas datums")]
        public DateTime BirthDate { set; get; }

        [StringLength(20, ErrorMessage = "Dzimums par garu")]
        [Required(ErrorMessage = "Dzimums ir obligāta.")]
        [GenderValidation(ErrorMessage = "Dzimums ir nepareizs.")]
        [Display(Name = "Dzimums")]
        public string Gender { set; get; }

        [StringLength(100, ErrorMessage = "Sugas nosaukums par garu")]
        [Required(ErrorMessage = "Suga ir obligāta.")]
        [Display(Name = "Suga")]
        public string Species { set; get; }

        [StringLength(100, ErrorMessage = "Šķirnes nosaukums par garu")]
        [Display(Name = "Šķirne")]
        public string SpeciesType { set; get; }

        [StringLength(100, ErrorMessage = "Krāsas nosaukums par garu")]
        [Required(ErrorMessage = "Krāsa ir obligāta.")]
        [Display(Name = "Krāsa")]
        public string Colour { set; get; }

        [StringLength(100, ErrorMessage = "Sekundārās krāsas nosaukums par garu")]
        [Display(Name = "Sekundārā krāsa")]
        public string SecondaryColour { set; get; }

        [StringLength(5000, ErrorMessage = "Apraksts pārsniedz 5000 maksimālo garumu")]
        [Display(Name = "Apraksts")]
        public string About { set; get; }

        public string ImagePath { set; get; }

        [Required(ErrorMessage = "Svars ir obligāts.")]
        [Range(0.01, 250, ErrorMessage = "Svars Jābūt lielākam par 0 un mazākam par 250 Kg.")]
        [Display(Name = "Svars (Kg)")]
        public double Weight { set; get; }

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        [ShelterValidation(ErrorMessage = "Šī patversme neeksistē")]
        [Required(ErrorMessage = "Patversme ir obligāta.")]
        public string ShelterId { set; get; }

        [Display(Name = "Patversme")]
        public string ShelterName {set; get; }

        [Display(Name = "E-pasts")]
        [UIHint("tinymce_jquery_full")]
        public string EmailMessage { get; set; }

        [Display(Name = "E-pasta tituls")]
        public string EmailTitle { get; set; }
    }
}