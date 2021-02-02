using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using WebPatversme.Models;
using WebPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebPatversme.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsServices;
        private readonly IMapper _mapper;

        public NewsController(
            INewsService newsServices,
            IMapper mapper)
        {
            _newsServices = newsServices;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var mappedNews = _mapper.Map<List<NewsViewModel>>(_newsServices.NewsList());

            return View(mappedNews);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedNews = _mapper.Map<News>(model);

                _newsServices.AddNewNews(mappedNews);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(string Id)
        {
            var allNews =  _mapper.Map<List<NewsViewModel>>(_newsServices.NewsList());

            var returningNews = allNews.Where(news => news.NewsID == Id).FirstOrDefault();

            return View(returningNews);
        }

        [HttpPost]
        public IActionResult Edit(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedNews = _mapper.Map<News>(model);

                _newsServices.AddNewNews(mappedNews);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}