using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Models.ViewModels.Animal;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class EditAnimalCreationViewModel
    {
        [BindProperty]
        public List<AnimalColourViewModel> ColourList { get; set; }

        [BindProperty]
        public List<AnimalSpeciesViewModel> SpeciesList { get; set; }

        [BindProperty]
        public List<AnimalSpeciesTypeViewModel> SpeciesTypesList { get; set; }

        public AnimalColourViewModel Colour { get; set; }

        public AnimalSpeciesViewModel Species { get; set; }

        public AnimalSpeciesTypeViewModel SpeciesType { get; set; }
    }
}