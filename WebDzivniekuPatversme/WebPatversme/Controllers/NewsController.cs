using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Services.Other;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Models.ViewModels.News;

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

        [AllowAnonymous]
        public IActionResult Index(
            string sortOrder,
            string name,
            int? pageNumber,
            int pageSize = 3)
        {
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["DateAddedSortParm"] = sortOrder == "dateAdded" ? "dateAdded_desc" : "dateAdded";

            ViewData["Name"] = name;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["PageSize"] = pageSize;

            var allNews = _newsServices.GetAllNewsList();
            var mappedNews = _mapper.Map<List<NewsViewModel>>(allNews);

            mappedNews = _newsServices.SortNews(mappedNews, sortOrder, name);

            ViewData["PageAmount"] = Decimal.ToInt32(Math.Ceiling(mappedNews.Count / (decimal)pageSize)) + 1;

            return View(PaginatedList<NewsViewModel>.Create(mappedNews, pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "administrator,worker")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Create(NewsCreateViewModel news)
        {
            if (ModelState.IsValid)
            {
                var mappedNews = _mapper.Map<News>(news);

                mappedNews.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _newsServices.AddNewNews(mappedNews);

                return RedirectToAction("Index");
            }

            return View(news);
        }

        [Authorize(Roles = "administrator,worker")]
        public IActionResult Edit(string Id)
        {
            var news = _newsServices.GetNewsById(Id);
            var mappedNews = _mapper.Map<NewsEditViewModel>(news);

            return View(mappedNews);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Edit(NewsEditViewModel news)
        {
            if (ModelState.IsValid)
            {
                var mappedNews = _mapper.Map<News>(news);

                _newsServices.EditNews(mappedNews);

                return RedirectToAction("Index");
            }

            return View(news);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Delete(NewsDetailsViewModel news)
        {
            var mappedNews = _mapper.Map<News>(news);

            _newsServices.DeleteNews(mappedNews);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult Details(string Id)
        {
            var news = _newsServices.GetNewsById(Id);
            var mappedNews = _mapper.Map<NewsDetailsViewModel>(news);

            return View(mappedNews);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}