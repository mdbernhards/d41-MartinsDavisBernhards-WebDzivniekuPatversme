using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;
using System.Collections.Generic;
using WebPatversme.Models;

namespace WebDzivniekuPatversme.Services
{
    public class ShelterServices : IShelterServices
    {
        private readonly IShelterRepository _shelterRepository;

        public ShelterServices(
            IShelterRepository shelterRepository)
        {
            _shelterRepository = shelterRepository;
        }

        public List<AnimalShelters> AnimalShelterTable()
        {
            return _shelterRepository.GetAllAnimalShelters();
        }
    }
}