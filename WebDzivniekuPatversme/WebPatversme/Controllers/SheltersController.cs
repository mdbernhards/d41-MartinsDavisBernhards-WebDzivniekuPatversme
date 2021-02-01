using System.Linq;
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
            return View(_sheltersServices.ShelterList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Shelters model)
        {
            if (ModelState.IsValid)
            {
                _sheltersServices.AddNewShelter(model);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(string ShelterId)
        {
            var allShelters = _sheltersServices.ShelterList();

            var returningShelter = allShelters.Where(shelter => shelter.AnimalShelterID == ShelterId).FirstOrDefault();

            return View(returningShelter);
        }

        [HttpPost]
        public IActionResult Edit(Shelters shelters)
        {
            if (ModelState.IsValid)
            {
                _sheltersServices.AddNewShelter(shelters);

                return RedirectToAction("Index");
            }
            return View(shelters);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}