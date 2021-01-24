using System.Diagnostics;
using WebPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebPatversme.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsServices _newsServices;

        public NewsController(
            INewsServices newsServices)
        {
            _newsServices = newsServices;
        }

        public IActionResult Index()
        {
            return View(_newsServices.NewsTable());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}