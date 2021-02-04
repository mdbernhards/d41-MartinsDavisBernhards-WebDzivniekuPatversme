using System;
using System.Linq;
using WebDzivniekuPatversme.Models;
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

        public List<News> GetAllNewsList()
        {
            var newsList = _newsRepository.GetAllNews();

            return newsList;
        }

        public News GetNewsById(string Id)
        {
            var newsList = _newsRepository.GetAllNews();
            var news = newsList.Where(animal => animal.NewsID == Id).FirstOrDefault();

            return news;
        }

        public void DeleteNews(News news)
        {
            _newsRepository.DeleteNews(news);
        }

        public void AddNewNews(News news)
        {
            news.NewsID = Guid.NewGuid().ToString();
            news.DateCreated = DateTime.Now;

            _newsRepository.CreateNewNews(news);
        }

        public void EditNews(News news)
        {
            _newsRepository.EditNews(news);
        }
    }
}