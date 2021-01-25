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

        public List<News> NewsTable()
        {
            return _newsRepository.GetAllNews();
        }

        public void AddNewNews(News news)
        {
            _newsRepository.CreateNewNews(news);
        }
    }
}