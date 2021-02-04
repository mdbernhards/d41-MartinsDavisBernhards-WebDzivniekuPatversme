using System;
using System.Linq;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
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

        public List<Shelters> GetAllShelterList()
        {
            var shelterList = _shelterRepository.GetAllAnimalShelters();

            return shelterList;
        }

        public Shelters GetShelterById(string Id)
        {
            var shelterList = _shelterRepository.GetAllAnimalShelters();
            var shelter = shelterList.Where(animal => animal.AnimalShelterID == Id).FirstOrDefault();

            return shelter;
        }

        public void DeleteShelter(Shelters shelter)
        {
            _shelterRepository.DeleteShelters(shelter);
        }

        public void AddNewShelter(Shelters shelter)
        {
            shelter.AnimalShelterID = Guid.NewGuid().ToString();

            _shelterRepository.CreateNewAnimalShelter(shelter);
        }

        public void EditShelter(Shelters shelter)
        {
            _shelterRepository.EditShelter(shelter);
        }
    }
}