using System.Linq;
using System.Diagnostics;
using WebPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Services.Interfaces;
using AutoMapper;
using WebPatversme.Models.ViewModels;
using System.Collections.Generic;

namespace WebPatversme.Controllers
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
            var mappedShelters = _mapper.Map<List<SheltersViewModel>>(_sheltersServices.ShelterList());

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
            var allShelters = _sheltersServices.ShelterList();

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}