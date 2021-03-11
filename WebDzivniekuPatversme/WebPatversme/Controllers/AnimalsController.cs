using System;
using System.Diagnostics;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Services.Other;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Models.ViewModels.Animal;
using WebDzivniekuPatversme.Models.ViewModels.Shelter;

namespace WebDzivniekuPatversme.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IAnimalsService _animalsServices;
        private readonly IMapper _mapper;

        public AnimalsController(
            IAnimalsService animalsServices,
            IMapper mapper)
        {
            _animalsServices = animalsServices;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult Index(
            string sortOrder,
            int? pageNumber,
            string name,
            string age,
            string species,
            string speciesType,
            string colour,
            string shelter,
            int pageSize = 3
            )
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AgeSortParm"] = sortOrder == "age" ? "age_desc" : "age";
            ViewData["SpeciesSortParm"] = sortOrder == "species" ? "species_desc" : "species";
            ViewData["SpeciesTypeSortParm"] = sortOrder == "speciesType" ? "speciesType_desc" : "speciesType";
            ViewData["WeightSortParm"] = sortOrder == "weight" ? "weight_desc" : "weight";
            ViewData["ShelterSortParm"] = sortOrder == "shelter" ? "shelter_desc" : "shelter";
            ViewData["DateAddedSortParm"] = sortOrder == "dateAdded" ? "dateAdded_desc" : "dateAdded";
            ViewData["ColourSortParm"] = sortOrder == "colour" ? "colour_desc" : "colour";

            ViewData["CurrentSort"] = sortOrder;
            ViewData["PageSize"] = pageSize;

            ViewData["Name"] = name;
            ViewData["Age"] = age;
            ViewData["Species"] = species;
            ViewData["SpeciesType"] = speciesType;
            ViewData["Colour"] = colour;
            ViewData["Shelter"] = shelter;

            var filter = _animalsServices.CreateAnimalFilter(name, age, species, speciesType, colour, shelter);
            var animalList = _mapper.Map<List<AnimalViewModel>>(_animalsServices.GetAllAnimalList());

            animalList = _animalsServices.AddAnimalShelterNames(animalList);

            ViewData["DropDown"] = _animalsServices.CreateAnimalDropDownListValues(animalList, filter);

            animalList = _animalsServices.FilterAndSortAnimals(animalList, sortOrder, filter);
            ViewData["PageAmount"] = Decimal.ToInt32(Math.Ceiling(animalList.Count / (decimal)pageSize)) + 1;

            return View(PaginatedList<AnimalViewModel>.Create(animalList, pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "administrator,worker")]
        public IActionResult Create()
        {
            AnimalViewModel animalModel = new AnimalViewModel
            {
                AnimalShelters = _mapper.Map<List<ShelterViewModel>>(_animalsServices.GetAllShelters()),
                AnimalColours = _animalsServices.GetAllColours(),
                AnimalSpecies = _animalsServices.GetAllSpecies(),
                AnimalSpeciesTypes = _animalsServices.GetAllSpeciesTypes(),
                BirthDate = DateTime.Today,
                BirthDateRangeTo = DateTime.Today,
            };

            return View(animalModel);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Create(AnimalViewModel animal)
        {
            if (ModelState.IsValid)
            {
                var mappedAnimal = _mapper.Map<Animal>(animal);

                _animalsServices.AddNewAnimal(mappedAnimal);

                return RedirectToAction("Index");
            }

            animal.AnimalColours = _animalsServices.GetAllColours();
            animal.AnimalSpecies = _animalsServices.GetAllSpecies();
            animal.AnimalSpeciesTypes = _animalsServices.GetAllSpeciesTypes();
            animal.AnimalShelters = _mapper.Map<List<ShelterViewModel>>(_animalsServices.GetAllShelters());

            return View(animal);
        }

        [Authorize(Roles = "administrator,worker")]
        public IActionResult Edit(string id)
        {
            var animal = _animalsServices.GetAnimalById(id);
            var mappedAnimal = _mapper.Map<AnimalViewModel>(animal);

            mappedAnimal.AnimalShelters = _mapper.Map<List<ShelterViewModel>>(_animalsServices.GetAllShelters());
            mappedAnimal.AnimalColours = _animalsServices.GetAllColours();
            mappedAnimal.AnimalSpecies = _animalsServices.GetAllSpecies();
            mappedAnimal.AnimalSpeciesTypes = _animalsServices.GetAllSpeciesTypes();

            return View(mappedAnimal);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Edit(AnimalViewModel animal)
        {
            if (ModelState.IsValid)
            {
                var mappedAnimal = _mapper.Map<Animal>(animal);

                _animalsServices.EditAnimal(mappedAnimal);

                return RedirectToAction("Index");
            }

            animal.AnimalColours = _animalsServices.GetAllColours();
            animal.AnimalSpecies = _animalsServices.GetAllSpecies();
            animal.AnimalSpeciesTypes = _animalsServices.GetAllSpeciesTypes();
            animal.AnimalShelters = _mapper.Map<List<ShelterViewModel>>(_animalsServices.GetAllShelters());

            return View(animal);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Delete(AnimalViewModel model)
        {
            var mappedAnimals = _mapper.Map<Animal>(model);

            _animalsServices.DeleteAnimal(mappedAnimals);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            var animal = _animalsServices.GetAnimalById(id);
            var mappedAnimal = _mapper.Map<AnimalViewModel>(animal);

            mappedAnimal = _animalsServices.AddAnimalShelterNames(mappedAnimal);

            return View(mappedAnimal);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker,user")]
        public IActionResult Details(AnimalViewModel model)
        {
            var mappedAnimal = _mapper.Map<Animal>(model);

            _animalsServices.SendAnimalEmail(mappedAnimal);
        
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}