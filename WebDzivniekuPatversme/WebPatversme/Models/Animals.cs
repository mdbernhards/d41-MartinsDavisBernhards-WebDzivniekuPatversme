using System;
using System.ComponentModel.DataAnnotations;

namespace WebPatversme.Models
{
    public class Animals
    {
        [Key]
        public string AnimalID { set; get; }

        [Required]
        public string Name { set; get; }

        public int Age { set; get; }

        [Required]
        public DateTime BirthDate { set; get; }

        [Required]
        public string Species { set; get; }

        [Required]
        public string Colour { set; get; }

        [Required]
        public string About { set; get; }

        public string ImagePath { set; get; }

        [Required]
        public int Weight { set; get; }

        [Required]
        public DateTime DateAdded { set; get; }

        [Required]
        public string FKAnimalSheltersID { set; get; }
    }
}