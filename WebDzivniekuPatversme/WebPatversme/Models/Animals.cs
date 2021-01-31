using System;
using System.ComponentModel.DataAnnotations;

namespace WebPatversme.Models
{
    public class Animals
    {
        [Key]
        public string AnimalID { set; get; }

        public string Name { set; get; }

        public int Age { set; get; }

        public DateTime BirthDate { set; get; }

        public string Species { set; get; }

        public string Colour { set; get; }

        public string About { set; get; }

        public string ImagePath { set; get; }

        public int Weight { set; get; }

        public DateTime DateAdded { set; get; }

        public string AnimalShelterId { set; get; }
    }
}