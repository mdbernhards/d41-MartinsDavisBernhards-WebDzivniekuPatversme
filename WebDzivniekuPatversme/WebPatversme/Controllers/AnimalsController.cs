using AutoMapper;
using System.Linq;
using System.Diagnostics;
using WebPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using WebPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Services.Interfaces;
using System.Collections.Generic;

namespace WebPatversme.Controllers
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
            var mappedAnimal = _mapper.Map<List<AnimalsViewModel>>(_animalsServices.AnimalList());

            return View(mappedAnimal);
        }

        public IActionResult Delete()
        {
            var mappedAnimal = _mapper.Map< List<AnimalsViewModel>>(_animalsServices.AnimalList());

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
            var allAnimals = _animalsServices.AnimalList();

            var returningAnimal = allAnimals.Where(animal => animal.AnimalID == id).FirstOrDefault();

            var mappedAnimal = _mapper.Map<AnimalsViewModel>(returningAnimal);

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}