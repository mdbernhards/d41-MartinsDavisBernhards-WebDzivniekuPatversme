using System.Diagnostics;
using WebPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebPatversme.Models.Database;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebPatversme.Controllers
{
    public class SheltersController : Controller
    {
        private readonly ILogger<SheltersController> _logger;
        private readonly IShelterServices _sheltersServices;

        public SheltersController(
            ILogger<SheltersController> logger,
            IShelterServices shelterServices)
        {
            _logger = logger;
            _sheltersServices = shelterServices;
        }

        public IActionResult Index()
        {
            ShelterRepository context = HttpContext.RequestServices.GetService(typeof(ShelterRepository)) as ShelterRepository;

            return View(context.GetAllAnimalShelters());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}