using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;
using System.Collections.Generic;
using WebPatversme.Models;

namespace WebDzivniekuPatversme.Services
{
    public class ShelterService : IShelterService
    {
        private readonly IShelterRepository _shelterRepository;

        public ShelterService(
            IShelterRepository shelterRepository)
        {
            _shelterRepository = shelterRepository;
        }

        public List<AnimalShelters> AnimalShelterTable()
        {
            return _shelterRepository.GetAllAnimalShelters();
        }

        public void AddNewShelter(AnimalShelters shelter)
        {
            _shelterRepository.CreateNewAnimalShelter(shelter);
        }
    }
}