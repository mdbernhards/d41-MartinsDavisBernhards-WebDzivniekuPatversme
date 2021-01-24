using WebPatversme.Models;
using System.Collections.Generic;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebDzivniekuPatversme.Services
{
    public class NewsServices : INewsServices
    {
        private readonly INewsRepository _newsRepository;

        public NewsServices(
            INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public List<News> NewsTable()
        {
            return _newsRepository.GetAllNews();
        }
    }
}