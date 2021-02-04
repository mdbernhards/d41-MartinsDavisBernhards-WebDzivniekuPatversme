using System.Diagnostics;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

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
        public IActionResult Index()
        {
            var allAnimals = _animalsServices.GetAllAnimalList();
            var mappedAnimals = _mapper.Map<List<AnimalsViewModel>>(allAnimals);

            return View(mappedAnimals);
        }

        [Authorize]
        public IActionResult Create()
        {
            AnimalsViewModel animalModel = new AnimalsViewModel
            {
                AnimalShelters = _animalsServices.GetAllShelters()
            };

            return View(animalModel);
        }

        [HttpPost]
        [Authorize]
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

        [Authorize]
        public IActionResult Edit(string id)
        {
            var animal = _animalsServices.GetAnimalById(id);
            var mappedAnimal = _mapper.Map<AnimalsViewModel>(animal);

            mappedAnimal.AnimalShelters = _animalsServices.GetAllShelters();

            return View(mappedAnimal);
        }

        [HttpPost]
        [Authorize]
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

        [Authorize]
        public IActionResult Delete(string id)
        {
            var animal = _animalsServices.GetAnimalById(id);
            var mappedAnimal = _mapper.Map<AnimalsViewModel>(animal);

            return View(mappedAnimal);
        }

        [HttpPost]
        [Authorize]
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

            return View(mappedAnimal);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}