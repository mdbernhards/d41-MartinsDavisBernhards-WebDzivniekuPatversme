using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Services.Interfaces;
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

        public IActionResult Index()
        {
            var mappedAnimal = _mapper.Map<List<AnimalsViewModel>>(_animalsServices.GetAllAnimalList());

            return View(mappedAnimal);
        }

        public IActionResult Create()
        {
            return View(_animalsServices.ObjectForCreatingAnimal());
        }

        [HttpPost]
        public IActionResult Create(AnimalsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedAnimal = _mapper.Map<Animals>(model);

                _animalsServices.AddNewAnimal(mappedAnimal);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(string id)
        {
            var animal = _animalsServices.GetAnimalById(id);

            var mappedAnimal = _mapper.Map<AnimalsViewModel>(animal);

            return View(mappedAnimal);
        }

        [HttpPost]
        public IActionResult Edit(AnimalsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedAnimal = _mapper.Map<Animals>(model);

                _animalsServices.AddNewAnimal(mappedAnimal);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(string Id)
        {
            var allAnimals = _mapper.Map<List<AnimalsViewModel>>(_animalsServices.GetAllAnimalList());

            var returningAnimals = allAnimals.Where(shelters => shelters.AnimalID == Id).FirstOrDefault();

            return View(returningAnimals);
        }

        [HttpPost]
        public IActionResult Delete(AnimalsViewModel model)
        {
            var mappedAnimals = _mapper.Map<Animals>(model);

            _animalsServices.DeleteAnimals(mappedAnimals);

            return RedirectToAction("Index");
        }

        public IActionResult Details(string Id)
        {
            var allAnimals = _mapper.Map<List<AnimalsViewModel>>(_animalsServices.GetAllAnimalList());

            var returningAnimals = allAnimals.Where(shelters => shelters.AnimalID == Id).FirstOrDefault();

            return View(returningAnimals);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}