using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebDzivniekuPatversme.Controllers
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
            var allNews = _newsServices.GetAllNewsList();
            var mappedNews = _mapper.Map<List<NewsViewModel>>(allNews);

            return View(mappedNews);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewsViewModel news)
        {
            if (ModelState.IsValid)
            {
                var mappedNews = _mapper.Map<News>(news);

                _newsServices.AddNewNews(mappedNews);

                return RedirectToAction("Index");
            }
            return View(news);
        }

        public IActionResult Edit(string Id)
        {
            var news = _newsServices.GetNewsById(Id);
            var mappedNews = _mapper.Map<NewsViewModel>(news);

            return View(mappedNews);
        }

        [HttpPost]
        public IActionResult Edit(NewsViewModel news)
        {
            if (ModelState.IsValid)
            {
                var mappedNews = _mapper.Map<News>(news);

                _newsServices.EditNews(mappedNews);

                return RedirectToAction("Index");
            }
            return View(news);
        }

        public IActionResult Delete(string Id)
        {
            var news = _newsServices.GetNewsById(Id);
            var mappedNews = _mapper.Map<NewsViewModel>(news);

            return View(mappedNews);
        }

        [HttpPost]
        public IActionResult Delete(NewsViewModel news)
        {
            var mappedNews = _mapper.Map<News>(news);

            _newsServices.DeleteNews(mappedNews);

            return RedirectToAction("Index");
        }

        public IActionResult Details(string Id)
        {
            var news = _newsServices.GetNewsById(Id);
            var mappedNews = _mapper.Map<NewsViewModel>(news);

            return View(mappedNews);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}