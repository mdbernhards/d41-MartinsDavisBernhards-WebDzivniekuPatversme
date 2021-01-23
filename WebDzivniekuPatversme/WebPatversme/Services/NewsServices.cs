using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebPatversme.Models.Database
{
    public class NewsServices : INewsServices
    {
        private readonly INewsRepository _newsRepository;

        public NewsServices(
            INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
    }
}