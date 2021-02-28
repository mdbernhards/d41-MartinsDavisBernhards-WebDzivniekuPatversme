using System;
using Microsoft.AspNetCore.Http;

namespace WebDzivniekuPatversme.Models
{
    public class Animals
    {
        public string AnimalID { set; get; }

        public string Name { set; get; }

        public string Age { set; get; }

        public DateTime? BirthDate { set; get; }

        public DateTime? BirthDateRangeTo { set; get; }

        public string Species { set; get; }

        public string SpeciesType { set; get; }

        public string Colour { set; get; }

        public string SecondaryColour { set; get; }

        public string About { set; get; }

        public string ImagePath { set; get; }

        public IFormFile Image { set; get; }

        public double Weight { set; get; }

        public DateTime DateAdded { set; get; }

        public string EmailMessage { get; set; }

        public string EmailTitle { get; set; }

        public string AnimalShelterId { set; get; }
    }
}