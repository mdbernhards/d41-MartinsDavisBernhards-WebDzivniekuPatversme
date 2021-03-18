using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Models.ViewModels.Animal;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class EditAnimalCreationViewModel
    {
        [BindProperty]
        public List<AnimalColourViewModel> Colours { get; set; }

        [BindProperty]
        public List<AnimalSpeciesViewModel> Species { get; set; }

        [BindProperty]
        public List<AnimalSpeciesTypeViewModel> SpeciesTypes { get; set; }
    }
}