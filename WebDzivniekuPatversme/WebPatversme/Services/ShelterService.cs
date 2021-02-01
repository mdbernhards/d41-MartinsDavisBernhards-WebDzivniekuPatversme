using System;
using System.Collections.Generic;
using WebPatversme.Models;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

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

        public List<Shelters> ShelterList()
        {
            return _shelterRepository.GetAllAnimalShelters();
        }

        public void AddNewShelter(Shelters shelter)
        {
            shelter.AnimalShelterID = Guid.NewGuid().ToString();

            _shelterRepository.CreateNewAnimalShelter(shelter);
        }
    }
}