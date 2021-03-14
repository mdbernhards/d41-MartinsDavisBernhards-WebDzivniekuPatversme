using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebDzivniekuPatversme.Validation;

namespace WebDzivniekuPatversme.Models.ViewModels.Animal
{
    public class AnimalEditViewModel
    {
        [Key]
        public string Id { set; get; }

        [StringLength(100, ErrorMessage = "Vārds par garu!")]
        [Required(ErrorMessage = "Vārds ir obligāts!")]
        [Display(Name = "Vārds")]
        public string Name { set; get; }

        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Datums nevar būt nākotnē!")]
        [Display(Name = "Dzimšanas datums")]
        public DateTime BirthDate { set; get; }

        [StringLength(20, ErrorMessage = "Dzimums par garu!")]
        [Required(ErrorMessage = "Dzimums ir obligāta!")]
        [GenderValidation(ErrorMessage = "Dzimums ir nepareizs!")]
        [Display(Name = "Dzimums")]
        public string Gender { set; get; }

        [StringLength(100, ErrorMessage = "Sugas nosaukums par garu!")]
        [Required(ErrorMessage = "Suga ir obligāta!")]
        [SpeciesValidation(false, ErrorMessage = "Šī suga datubāzē neeksistē!")]
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

        [StringLength(5000, ErrorMessage = "Apraksts pārsniedz 5000 maksimālo garumu!")]
        [Display(Name = "Apraksts")]
        public string About { set; get; }

        public string ImagePath { set; get; }

        [Display(Name = "Attēls")]
        [DataType(DataType.Upload)]
        [MaxFileSizeValidation(6 * 1024 * 1024)]
        [ExtensionValidation(new string[] { ".jpg", ".png", ".jpeg", ".gif", ".tif" })]
        public IFormFile Image { set; get; }

        [Required(ErrorMessage = "Svars ir obligāts!")]
        [Range(0.01, 250, ErrorMessage = "Svars Jābūt lielākam par 0 un mazākam par 250 Kg!")]
        [Display(Name = "Svars (Kg)")]
        public double Weight { set; get; }

        [ShelterValidation(ErrorMessage = "Šī patversme neeksistē!")]
        [Required(ErrorMessage = "Patversme ir obligāta!")]
        [Display(Name = "Patversme")]
        public string ShelterId { set; get; }

        [Display(Name = "Patversme")]
        public string ShelterName {set; get; }

        public IEnumerable<Shelter> AnimalShelters { get; set; }

        public IEnumerable<AnimalColour> AnimalColours { get; set; }

        public IEnumerable<AnimalSpecies> AnimalSpecies { get; set; }

        public IEnumerable<AnimalSpeciesType> AnimalSpeciesTypes { get; set; }
    }
}