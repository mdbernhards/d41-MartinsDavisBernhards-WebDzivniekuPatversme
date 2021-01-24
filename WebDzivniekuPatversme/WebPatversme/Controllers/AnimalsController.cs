using System.Diagnostics;
using WebPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebPatversme.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IAnimalsServices _animalsServices;

        public AnimalsController(
            IAnimalsServices animalsServices)
        {
            _animalsServices = animalsServices;
        }

        public IActionResult Index()
        {
            return View(_animalsServices.AnimalsTable());
        }

        public IActionResult Animal()
        {
            Animals animal = new Animals();
            return View(animal);
        }


        [HttpPost]
        public IActionResult CreateAnimal(Animals animal)
        {
            _animalsServices.AddNewAnimal(animal);

            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}