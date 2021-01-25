using System.Diagnostics;
using WebPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebPatversme.Controllers
{
    public class SheltersController : Controller
    {
        private readonly IShelterService _sheltersServices;

        public SheltersController(
            IShelterService shelterServices)
        {
            _sheltersServices = shelterServices;
        }

        public IActionResult Index()
        {
            return View(_sheltersServices.AnimalShelterTable());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AnimalShelters model)
        {
            _sheltersServices.AddNewShelter(model);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}