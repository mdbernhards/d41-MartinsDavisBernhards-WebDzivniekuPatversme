using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebPatversme.Models.Database
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