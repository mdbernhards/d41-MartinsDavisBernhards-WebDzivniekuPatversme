using System.Diagnostics;
using WebPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Services.Interfaces;
using WebPatversme.Models.ViewModels;
using AutoMapper;

namespace WebPatversme.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IAnimalsService _animalsServices;

        public AnimalsController(
            IAnimalsService animalsServices)
        {
            _animalsServices = animalsServices;
        }

        public IActionResult Index()
        {
            return View(_animalsServices.AnimalList());
        }

        public IActionResult Delete()
        {
            return View(_animalsServices.AnimalList());
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
                Mapper mapper = new Mapper(null);

                var mappedAnimal = mapper.Map<AnimalsViewModel, Animals>(model);

                _animalsServices.AddNewAnimal(mappedAnimal);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Animals model)
        {
            if (ModelState.IsValid)
            {
                _animalsServices.AddNewAnimal(model);

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