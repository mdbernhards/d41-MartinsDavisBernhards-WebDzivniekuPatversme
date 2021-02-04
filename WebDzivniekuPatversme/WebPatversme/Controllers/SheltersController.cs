using System.Diagnostics;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

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
            var allShelters = _sheltersServices.GetAllShelterList();
            var mappedShelters = _mapper.Map<List<SheltersViewModel>>(allShelters);

            return View(mappedShelters);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SheltersViewModel shelter)
        {
            if (ModelState.IsValid)
            {
                var mappedShelter = _mapper.Map<Shelters>(shelter);

                _sheltersServices.AddNewShelter(mappedShelter);

                return RedirectToAction("Index");
            }
            return View(shelter);
        }

        public IActionResult Edit(string Id)
        {
            var shelter = _sheltersServices.GetShelterById(Id);
            var mappedShelter = _mapper.Map<SheltersViewModel>(shelter);

            return View(mappedShelter);
        }

        [HttpPost]
        public IActionResult Edit(SheltersViewModel shelter)
        {
            if (ModelState.IsValid)
            {
                var mappedShelter = _mapper.Map<Shelters>(shelter);

                _sheltersServices.EditShelter(mappedShelter);

                return RedirectToAction("Index");
            }
            return View(shelter);
        }

        public IActionResult Delete(string Id)
        {
            var shelter = _sheltersServices.GetShelterById(Id);
            var mappedShelter = _mapper.Map<SheltersViewModel>(shelter);

            return View(mappedShelter);
        }

        [HttpPost]
        public IActionResult Delete(SheltersViewModel shelter)
        {
            var mappedShelter = _mapper.Map<Shelters>(shelter);

            _sheltersServices.DeleteShelter(mappedShelter);

            return RedirectToAction("Index");
        }

        public IActionResult Details(string Id)
        {
            var shelter = _sheltersServices.GetShelterById(Id);
            var mappedShelter = _mapper.Map<SheltersViewModel>(shelter);

            return View(mappedShelter);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}