using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDzivniekuPatversme.Repositories.Interfaces;
using WebDzivniekuPatversme.Models.ViewModels.Animal;
using WebDzivniekuPatversme.Models.ViewModels.Identity;

namespace WebDzivniekuPatversme.Areas.Identity.Pages.Account.Manage
{
    public class EditAnimalCreationModel : PageModel
    {
        private readonly IAnimalsRepository _animalsRepository;

        public EditAnimalCreationModel(
            IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;

            Input = new EditAnimalCreationViewModel();
        }

        public EditAnimalCreationViewModel Input { get; set; }

        public string SpeciesId { get; set; }

        public async Task<IActionResult> OnGetAsync(string speciesId)
        {
            Input.Colours = _animalsRepository.GetAllColours();
            Input.Species = _animalsRepository.GetAllSpecies();

            if (speciesId != null)
            {
                SpeciesId = speciesId;
            }
            else
            {
                SpeciesId = Input.Species.FirstOrDefault().Id;
            }

            Input.SpeciesTypes = _animalsRepository
                .GetAllSpeciesTypes()
                .Where(x => x.SpeciesId == SpeciesId)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAddColourAsync(AnimalColourViewModel colour)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            colour.Id = Guid
                .NewGuid()
                .ToString();

            _animalsRepository.CreateNewColour(colour);

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

        public async Task<IActionResult> OnPostAddSpeciesAsync(AnimalSpeciesViewModel species)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            species.Id = Guid
                .NewGuid()
                .ToString();

            _animalsRepository.CreateNewSpecies(species);

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

        public async Task<IActionResult> OnPostAddSpeciesTypeAsync(AnimalSpeciesTypeViewModel speciesType)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            speciesType.Id = Guid
                .NewGuid()
                .ToString();

            _animalsRepository.CreateNewSpeciesType(speciesType);

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