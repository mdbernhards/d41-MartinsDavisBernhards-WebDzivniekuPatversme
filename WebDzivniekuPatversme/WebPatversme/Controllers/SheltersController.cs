using System.Diagnostics;
using WebPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebPatversme.Controllers
{
    public class SheltersController : Controller
    {
        private readonly IShelterServices _sheltersServices;

        public SheltersController(
            IShelterServices shelterServices)
        {
            _sheltersServices = shelterServices;
        }

        public IActionResult Index()
        {
            return View(_sheltersServices.AnimalShelterTable());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}