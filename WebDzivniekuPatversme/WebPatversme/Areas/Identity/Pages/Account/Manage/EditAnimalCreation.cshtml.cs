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
        public List<AnimalSpecies> Spiecies { get; set; }

        [BindProperty]
        public List<AnimalSpeciesType> SpeciesTypes { get; set; }

        public AnimalColour InputColour { get; set; }

        public AnimalSpecies InputSpecies { get; set; }

        public AnimalSpeciesType InputSpieciesTypes { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Colours = _animalsRepository.GetAllColours();
            Spiecies = _animalsRepository.GetAllSpecies();
            SpeciesTypes = _animalsRepository.GetAllSpeciesTypes();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            string colour, 
            string species, 
            string speciesTypes)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage();
        }
    }
}