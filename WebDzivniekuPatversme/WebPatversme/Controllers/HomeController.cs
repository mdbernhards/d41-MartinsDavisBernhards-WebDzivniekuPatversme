using System.Diagnostics;
using WebPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebPatversme.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeServices;

        public HomeController(
            IHomeService homeServices)
        {
            _homeServices = homeServices;
        }

        public IActionResult Index()
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