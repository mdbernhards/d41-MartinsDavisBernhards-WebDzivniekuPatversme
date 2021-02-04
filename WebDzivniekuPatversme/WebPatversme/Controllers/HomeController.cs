using System.Diagnostics;
using WebDzivniekuPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebDzivniekuPatversme.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeServices;

        public HomeController(
            IHomeService homeServices)
        {
            _homeServices = homeServices;
        }

        [AllowAnonymous]
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