using System;
using WebPatversme.Models.Database;

namespace WebPatversme.Models
{
    public class Animals
    {
        private ShelterRepository context;

        public int AnimalID;

        public string Name;

        public int Age;

        public DateTime BirthDate;

        public string Species;

        public string Colour;

        public string About;

        public string ImagePath;

        public int Weight;

        public DateTime DateAdded;

        public int FKAnimalSheltersID;
    }
}