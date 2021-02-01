using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebPatversme.Models.ViewModels
{
    public class AnimalsViewModel
    {
        [Required(ErrorMessage = "Vārds ir obligāts.")]
        [Display(Name = "Vārds")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Vecums ir obligāts.")]
        [Display(Name = "Vecums")]
        public int Age { set; get; }

        [Required(ErrorMessage = "Dzimšanas datums ir obligāts.")]
        [Display(Name = "Dzimšanas datums")]
        public DateTime BirthDate { set; get; }

        [Required(ErrorMessage = "Suga ir obligāta.")]
        [Display(Name = "Suga")]
        public string Species { set; get; }

        [Required(ErrorMessage = "Krāsa ir obligāta.")]
        [Display(Name = "Krāsa")]
        public string Colour { set; get; }

        [Required(ErrorMessage = "Apraksts ir obligāts.")]
        [Display(Name = "Apraksts")]
        public string About { set; get; }

        public string ImagePath { set; get; }

        [Required(ErrorMessage = "Svars ir obligāts.")]
        [Display(Name = "Svars")]
        public int Weight { set; get; }

        public DateTime DateAdded { set; get; }

        public string AnimalShelterId { set; get; }

        public IEnumerable<Shelters> AnimalShelters { get; set; }
    }
}