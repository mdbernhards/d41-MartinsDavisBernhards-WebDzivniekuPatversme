using System.Linq;
using System.Diagnostics;
using WebDzivniekuPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Services.Interfaces;
using AutoMapper;
using WebDzivniekuPatversme.Models.ViewModels;
using System.Collections.Generic;

namespace WebDzivniekuPatversme.Controllers
{
    public class SheltersController : Controller
    {
        private readonly IShelterService _sheltersServices;
        private readonly IMapper _mapper;

        public SheltersController(
            IShelterService shelterServices,
            IMapper mapper)
        {
            _sheltersServices = shelterServices;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var mappedShelters = _mapper.Map<List<SheltersViewModel>>(_sheltersServices.GetAllShelterList());

            return View(mappedShelters);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SheltersViewModel shelters)
        {
            if (ModelState.IsValid)
            {
                var mappedShelter = _mapper.Map<Shelters>(shelters);

                _sheltersServices.AddNewShelter(mappedShelter);

                return RedirectToAction("Index");
            }
            return View(shelters);
        }

        public IActionResult Edit(string Id)
        {
            var allShelters = _sheltersServices.GetAllShelterList();

            var returningShelter = allShelters.Where(shelter => shelter.AnimalShelterID == Id).FirstOrDefault();

            var mappedShelter = _mapper.Map<SheltersViewModel>(returningShelter);

            return View(mappedShelter);
        }

        [HttpPost]
        public IActionResult Edit(SheltersViewModel shelters)
        {
            if (ModelState.IsValid)
            {
                var mappedShelter = _mapper.Map<Shelters>(shelters);

                _sheltersServices.AddNewShelter(mappedShelter);

                return RedirectToAction("Index");
            }
            return View(shelters);
        }

        public IActionResult Delete(string Id)
        {
            var allShelters = _mapper.Map<List<SheltersViewModel>>(_sheltersServices.GetAllShelterList());

            var returningShelters = allShelters.Where(shelters => shelters.AnimalShelterID == Id).FirstOrDefault();

            return View(returningShelters);
        }

        [HttpPost]
        public IActionResult Delete(SheltersViewModel model)
        {
            var mappedShelters = _mapper.Map<Shelters>(model);

            _sheltersServices.DeleteShelters(mappedShelters);

            return RedirectToAction("Index");
        }

        public IActionResult Details(string Id)
        {
            var allShelters = _mapper.Map<List<SheltersViewModel>>(_sheltersServices.GetAllShelterList());

            var returningShelters = allShelters.Where(shelters => shelters.AnimalShelterID == Id).FirstOrDefault();

            return View(returningShelters);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}