using System;
using System.Diagnostics;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Services.Other;
using WebDzivniekuPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Services.Interfaces;

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
            int age,
            string species,
            string colour,
            string shelter,
            int weight,
            int pageSize = 3
            )
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AgeSortParm"] = sortOrder == "age" ? "age_desc" : "age";
            ViewData["SpeciesSortParm"] = sortOrder == "species" ? "species_desc" : "species";
            ViewData["WeightSortParm"] = sortOrder == "weight" ? "weight_desc" : "weight";
            ViewData["ShelterSortParm"] = sortOrder == "shelter" ? "shelter_desc" : "shelter";
            ViewData["DateAddedSortParm"] = sortOrder == "dateAdded" ? "dateAdded_desc" : "dateAdded";
            ViewData["ColourSortParm"] = sortOrder == "colour" ? "colour_desc" : "colour";

            ViewData["CurrentSort"] = sortOrder;
            ViewData["PageSize"] = pageSize;

            ViewData["Name"] = name;
            ViewData["Age"] = age;
            ViewData["Species"] = species;
            ViewData["Colour"] = colour;
            ViewData["Shelter"] = shelter;
            ViewData["Weight"] = weight;

            var filter = _animalsServices.CreateAnimalFilter(name, age, species, colour, shelter, weight);

            var animalList = _animalsServices.GetAllAnimalList();
            var mappedAnimals = _mapper.Map<List<AnimalsViewModel>>(animalList);

            mappedAnimals = _animalsServices.AddAnimalShelterNames(mappedAnimals);
            ViewData["DropDown"] = _animalsServices.GetAnimalDropDownListValues(mappedAnimals);

            mappedAnimals = _animalsServices.FilterAndSortAnimals(mappedAnimals, sortOrder, filter);
            ViewData["PageAmount"] = Decimal.ToInt32(Math.Ceiling(mappedAnimals.Count / (decimal)pageSize)) + 1;

            return View(PaginatedList<AnimalsViewModel>.Create(mappedAnimals, pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "administrator,worker")]
        public IActionResult Create()
        {
            AnimalsViewModel animalModel = new AnimalsViewModel
            {
                AnimalShelters = _mapper.Map<List<SheltersViewModel>>(_animalsServices.GetAllShelters()),
                BirthDate = DateTime.Today
            };

            return View(animalModel);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Create(AnimalsViewModel animal)
        {
            if (ModelState.IsValid)
            {
                var mappedAnimal = _mapper.Map<Animals>(animal);

                _animalsServices.AddNewAnimal(mappedAnimal);

                return RedirectToAction("Index");
            }

            return View(animal);
        }

        [Authorize(Roles = "administrator,worker")]
        public IActionResult Edit(string id)
        {
            var animal = _animalsServices.GetAnimalById(id);
            var mappedAnimal = _mapper.Map<AnimalsViewModel>(animal);

            mappedAnimal.AnimalShelters = _mapper.Map<List<SheltersViewModel>>(_animalsServices.GetAllShelters());

            return View(mappedAnimal);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Edit(AnimalsViewModel animal)
        {
            if (ModelState.IsValid)
            {
                var mappedAnimal = _mapper.Map<Animals>(animal);

                _animalsServices.EditAnimal(mappedAnimal);

                return RedirectToAction("Index");
            }

            return View(animal);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Delete(AnimalsViewModel model)
        {
            var mappedAnimals = _mapper.Map<Animals>(model);

            _animalsServices.DeleteAnimal(mappedAnimals);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            var animal = _animalsServices.GetAnimalById(id);
            var mappedAnimal = _mapper.Map<AnimalsViewModel>(animal);

            mappedAnimal = _animalsServices.AddAnimalShelterNames(mappedAnimal);

            return View(mappedAnimal);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker,user")]
        public IActionResult Details(AnimalsViewModel model)
        {
            var mappedAnimals = _mapper.Map<Animals>(model);

            _animalsServices.SendAnimalEmail(mappedAnimals);
        
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}