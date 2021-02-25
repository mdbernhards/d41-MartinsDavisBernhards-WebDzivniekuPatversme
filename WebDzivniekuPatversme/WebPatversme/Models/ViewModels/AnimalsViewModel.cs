using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebDzivniekuPatversme.Services.Other;

namespace WebDzivniekuPatversme.Models.ViewModels
{
    public class AnimalsViewModel
    {
        [Key]
        public string AnimalID { set; get; }

        [StringLength(100, ErrorMessage = "Vārds par garu")]
        [Required(ErrorMessage = "Vārds ir obligāts.")]
        [Display(Name = "Vārds")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Vecums ir obligāts.")]
        [Display(Name = "Vecums")]
        public int Age { set; get; }

        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Datums nevar būt nākotnē")]
        [Required(ErrorMessage = "Dzimšanas datums ir obligāts.")]
        [Display(Name = "Dzimšanas datums")]
        public DateTime BirthDate { set; get; }

        [StringLength(100, ErrorMessage = "Sugas nosaukums par garu")]
        [Required(ErrorMessage = "Suga ir obligāta.")]
        [Display(Name = "Suga")]
        public string Species { set; get; }

        [StringLength(100, ErrorMessage = "Krāsas nosaukums par garu")]
        [Required(ErrorMessage = "Krāsa ir obligāta.")]
        [Display(Name = "Krāsa")]
        public string Colour { set; get; }

        [StringLength(5000, ErrorMessage = "Apraksts pārsniedz 5000 maksimālo garumu")]
        [Required(ErrorMessage = "Apraksts ir obligāts.")]
        [Display(Name = "Apraksts")]
        public string About { set; get; }

        public string ImagePath { set; get; }

        [Display(Name = "Attēls")]
        [DataType(DataType.Upload)]
        [MaxFileSizeValidation(6 * 1024 * 1024)]
        [ExtensionValidation(new string[] { ".jpg", ".png", ".jpeg", ".gif", ".tif" })]
        public IFormFile Image { set; get; }

        [Required(ErrorMessage = "Svars ir obligāts.")]
        [Range(0, 250, ErrorMessage = "Svars nevar būt mazāks par 0 un lielāks par 250.")]
        [Display(Name = "Svars (Kg)")]
        public int Weight { set; get; }

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        [Required(ErrorMessage = "Patversme ir obligāta.")]
        public string AnimalShelterId { set; get; }

        [Display(Name = "Patversme")]
        public string AnimalShelterName {set; get; }

        [Display(Name = "E-pasts")]
        [UIHint("tinymce_jquery_full")]
        public string EmailMessage { get; set; }

        [Display(Name = "E-pasta tituls")]
        public string EmailTitle { get; set; }

        public IEnumerable<SheltersViewModel> AnimalShelters { get; set; }
    }
}