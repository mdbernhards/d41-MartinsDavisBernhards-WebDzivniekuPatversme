using System;
using WebPatversme.Models;
using System.Collections.Generic;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebDzivniekuPatversme.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(
            INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public List<News> NewsList()
        {
            return _newsRepository.GetAllNews();
        }

        public void AddNewNews(News news)
        {
            news.NewsID = Guid.NewGuid().ToString();
            news.DateCreated = DateTime.Now;

            _newsRepository.CreateNewNews(news);
        }
    }
}