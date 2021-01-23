using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebPatversme.Models.Database
{
    public class ShelterServices : IShelterServices
    {
        private readonly IShelterRepository _shelterRepository;

        public ShelterServices(
            IShelterRepository shelterRepository)
        {
            _shelterRepository = shelterRepository;
        }
    }
}