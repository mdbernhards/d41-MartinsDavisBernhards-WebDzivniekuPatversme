using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebPatversme.Models.ViewModels
{
    public class AnimalsViewModel
    {
        [Required]
        public string Name { set; get; }

        [Required]
        public int Age { set; get; }

        [Required]
        public DateTime BirthDate { set; get; }

        [Required]
        public string Species { set; get; }

        [Required]
        public string Colour { set; get; }

        public string About { set; get; }

        public string ImagePath { set; get; }

        [Required]
        public int Weight { set; get; }

        public DateTime DateAdded { set; get; }

        public string AnimalShelterId { set; get; }

        public IEnumerable<Shelters> AnimalShelters { get; set; }
    }
}