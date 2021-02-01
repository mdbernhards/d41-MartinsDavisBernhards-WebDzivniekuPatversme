using System.Linq;
using System.Diagnostics;
using WebPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebPatversme.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsServices;

        public NewsController(
            INewsService newsServices)
        {
            _newsServices = newsServices;
        }

        public IActionResult Index()
        {
            return View(_newsServices.NewsList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(News model)
        {
            if (ModelState.IsValid)
            {
                _newsServices.AddNewNews(model);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(string NewsId)
        {
            var allNews = _newsServices.NewsList();

            var returningNews = allNews.Where(news => news.NewsID == NewsId).FirstOrDefault();

            return View(returningNews);
        }

        [HttpPost]
        public IActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                _newsServices.AddNewNews(news);

                return RedirectToAction("Index");
            }
            return View(news);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}