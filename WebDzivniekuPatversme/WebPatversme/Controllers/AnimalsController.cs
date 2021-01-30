using System.Diagnostics;
using WebPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Services.Interfaces;

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
            return View(_animalsServices.AnimalsTable());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Animals model)
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