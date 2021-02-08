using System.Collections.Generic;

namespace WebDzivniekuPatversme.Models.ViewModels
{
    public class AnimalsListViewModel
    {
        public List<AnimalsViewModel> AllAnimals { get; set; }

        public AnimalsViewModel Filter { get; set; }
    }
}