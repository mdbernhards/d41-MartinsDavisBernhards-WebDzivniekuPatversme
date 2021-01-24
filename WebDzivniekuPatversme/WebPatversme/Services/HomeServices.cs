using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebDzivniekuPatversme.Services
{
    public class HomeServices : IHomeServices
    {
        private readonly IHomeRepository _homeRepository;

        public HomeServices(
            IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }
    }
}