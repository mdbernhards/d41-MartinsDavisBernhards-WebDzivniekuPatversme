using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Repositories.Interfaces;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account.Manage
{
    public class EditAnimalCreationModel : PageModel
    {
        private readonly IAnimalsRepository _animalsRepository;

        public EditAnimalCreationModel(
            IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }

        [BindProperty]
        public List<AnimalColour> Colours { get; set; }

        [BindProperty]
        public List<AnimalSpecies> Species { get; set; }

        [BindProperty]
        public List<AnimalSpeciesType> SpeciesTypes { get; set; }

        public string SpeciesID { get; set; }

        public async Task<IActionResult> OnGetAsync(string speciesId)
        {
            Colours = _animalsRepository.GetAllColours();
            Species = _animalsRepository.GetAllSpecies();

            if (speciesId != null)
            {
                SpeciesID = speciesId;
            }
            else
            {
                SpeciesID = Species.FirstOrDefault().Id;
            }

            SpeciesTypes = _animalsRepository.GetAllSpeciesTypes()
                .Where(x => x.SpeciesId == SpeciesID)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAddColourAsync(string colourName)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newColour = new AnimalColour
            {
                Name = colourName,
                Id = Guid.NewGuid().ToString(),
            };

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _animalsRepository.CreateNewColour(newColour);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteColourAsync(string colourId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var animalColour = _animalsRepository
                .GetAllColours()
                .Where(x => x.Id == colourId)
                .FirstOrDefault();

            _animalsRepository.DeleteColour(animalColour);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddSpeciesAsync(string speciesName)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newSpecies = new AnimalSpecies
            {
                Name = speciesName,
                Id = Guid.NewGuid().ToString(),
            };

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _animalsRepository.CreateNewSpecies(newSpecies);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteSpeciesAsync(string speciesId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var animalSpecies = _animalsRepository
                .GetAllSpecies()
                .Where(x => x.Id == speciesId)
                .FirstOrDefault();

            var SpeciesTypes = _animalsRepository.
                GetAllSpeciesTypes()
                .Where(x => x.SpeciesId == speciesId)
                .ToList();

            foreach (var type in SpeciesTypes)
            {
                _animalsRepository.DeleteSpeciesType(type);
            }

            _animalsRepository.DeleteSpecies(animalSpecies);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddSpeciesTypeAsync(string speciesTypeName, string speciesId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newSpecies = new AnimalSpeciesType
            {
                Name = speciesTypeName,
                Id = Guid.NewGuid().ToString(),
                SpeciesId = speciesId,
            };

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _animalsRepository.CreateNewSpeciesType(newSpecies);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteSpeciesTypeAsync(string speciesTypeId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var animalSpeciesType = _animalsRepository
                .GetAllSpeciesTypes()
                .Where(x => x.Id == speciesTypeId)
                .FirstOrDefault();

            _animalsRepository.DeleteSpeciesType(animalSpeciesType);

            return RedirectToPage();
        }
    }
}