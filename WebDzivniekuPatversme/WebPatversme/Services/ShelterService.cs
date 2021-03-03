using System;
using System.Linq;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repositories.Interfaces;

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
            shelter.DateAdded = DateTime.Now;

            _shelterRepository.CreateNewAnimalShelter(shelter);
        }

        public void EditShelter(Shelters shelter)
        {
            _shelterRepository.EditShelter(shelter);
        }

        public List<SheltersViewModel> SortShelters(List<SheltersViewModel> shelters, string sortOrder, string searchString)
        {
            shelters = FilterShelters(shelters, searchString);
            shelters = OrderShelters(shelters, sortOrder);

            return shelters;
        }

        private List<SheltersViewModel> OrderShelters(List<SheltersViewModel> shelters, string sortOrder)
        {
            shelters = sortOrder switch
            {
                "name_desc" => shelters.OrderByDescending(s => s.Name).ToList(),
                "capacity" => shelters.OrderBy(s => s.AnimalCapacity).ToList(),
                "capacity_desc" => shelters.OrderByDescending(s => s.AnimalCapacity).ToList(),
                "address" => shelters.OrderBy(s => s.Address).ToList(),
                "address_desc" => shelters.OrderByDescending(s => s.Address).ToList(),
                "phoneNumber" => shelters.OrderBy(s => s.PhoneNumber).ToList(),
                "phoneNumber_desc" => shelters.OrderByDescending(s => s.PhoneNumber).ToList(),
                "dateAdded" => shelters.OrderBy(s => s.DateAdded).ToList(),
                "dateAdded_desc" => shelters.OrderByDescending(s => s.DateAdded).ToList(),
                _ => shelters.OrderBy(s => s.Name).ToList(),
            };
            return shelters;
        }

        private List<SheltersViewModel> FilterShelters(List<SheltersViewModel> shelters, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                shelters = shelters.Where(animal => animal.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return shelters;
        }
    }
}