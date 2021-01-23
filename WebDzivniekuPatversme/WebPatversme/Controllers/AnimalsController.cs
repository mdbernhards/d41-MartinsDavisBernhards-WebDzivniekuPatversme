using System.Diagnostics;
using WebPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebPatversme.Models.Database;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebPatversme.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly ILogger<AnimalsController> _logger;
        private readonly IAnimalsServices _animalsServices;

        public AnimalsController(
            ILogger<AnimalsController> logger,
            IAnimalsServices animalsServices)
        {
            _logger = logger;
            _animalsServices = animalsServices;
        }

        public IActionResult Index()
        {
            AnimalsRepository context = HttpContext.RequestServices.GetService(typeof(AnimalsRepository)) as AnimalsRepository;

            return View(context.GetAllAnimals());
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