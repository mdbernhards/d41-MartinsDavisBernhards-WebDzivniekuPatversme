using System.Diagnostics;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        public IActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CapacitySortParm = sortOrder == "capacity" ? "capacity_desc" : "capacity";
            ViewBag.AddressSortParm = sortOrder == "address" ? "address_desc" : "address";
            ViewBag.PhoneNumberSortParm = sortOrder == "phoneNumber" ? "phoneNumber_desc" : "phoneNumber";
            ViewBag.DateAddedSortParm = sortOrder == "dateAdded" ? "dateAdded_desc" : "dateAdded";

            var allShelters = _sheltersServices.GetAllShelterList();
            var mappedShelters = _mapper.Map<List<SheltersViewModel>>(allShelters);

            mappedShelters = _sheltersServices.SortShelters(mappedShelters, sortOrder);

            return View(mappedShelters);
        }

        [Authorize(Roles = "administrator,worker")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
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

        [Authorize(Roles = "administrator,worker")]
        public IActionResult Edit(string Id)
        {
            var shelter = _sheltersServices.GetShelterById(Id);
            var mappedShelter = _mapper.Map<SheltersViewModel>(shelter);

            return View(mappedShelter);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
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

        [Authorize(Roles = "administrator,worker")]
        public IActionResult Delete(string Id)
        {
            var shelter = _sheltersServices.GetShelterById(Id);
            var mappedShelter = _mapper.Map<SheltersViewModel>(shelter);

            return View(mappedShelter);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Delete(SheltersViewModel shelter)
        {
            var mappedShelter = _mapper.Map<Shelters>(shelter);

            _sheltersServices.DeleteShelter(mappedShelter);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
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