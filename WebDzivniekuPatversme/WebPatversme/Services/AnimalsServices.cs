using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebPatversme.Models.Database
{
    public class AnimalsServices : IAnimalsServices
    {
        private readonly IAnimalsRepository _animalsRepository;

        public AnimalsServices(
            IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }
    }
}