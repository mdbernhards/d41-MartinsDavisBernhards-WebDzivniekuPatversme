using System;
using System.Linq;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repositories.Interfaces;
using WebDzivniekuPatversme.Models.ViewModels.Shelters;

namespace WebDzivniekuPatversme.Services
{
    public class ShelterService : IShelterService
    {
        private readonly IShelterRepository _shelterRepository;
        private readonly IAnimalsRepository _animalRepository;

        public ShelterService(
            IShelterRepository shelterRepository,
            IAnimalsRepository animalRepository)
        {
            _shelterRepository = shelterRepository;
            _animalRepository = animalRepository;
        }

        public List<Shelter> GetAllShelters()
        {
            var shelterList = _shelterRepository.GetAllShelters();
            AddAnimalCount(shelterList);

            return shelterList;
        }

        public Shelter GetShelterById(string Id)
        {
            var shelterList = GetAllShelters();
            var shelter = shelterList
                .Where(animal => animal.Id == Id)
                .FirstOrDefault();

            return shelter;
        }

        public void DeleteShelter(Shelter shelter)
        {
            _shelterRepository.DeleteShelter(shelter);
        }

        public void AddNewShelter(Shelter shelter)
        {
            shelter.Id = Guid.NewGuid().ToString();
            shelter.DateAdded = DateTime.Now;

            _shelterRepository.CreateNewShelter(shelter);
        }

        public void EditShelter(Shelter shelter)
        {
            _shelterRepository.EditShelter(shelter);
        }

        public List<ShelterViewModel> FilterAndSortShelters(List<ShelterViewModel> shelters, string sortOrder, string searchString)
        {
            shelters = FilterShelters(shelters, searchString);
            shelters = OrderShelters(shelters, sortOrder);

            return shelters;
        }

        private static List<ShelterViewModel> OrderShelters(List<ShelterViewModel> shelters, string sortOrder)
        {
            shelters = sortOrder switch
            {
                "name_desc" => shelters
                    .OrderByDescending(s => s.Name)
                    .ToList(),
                "count" => shelters
                    .OrderBy(s => s.AnimalCount)
                    .ToList(),
                "count_desc" => shelters
                    .OrderByDescending(s => s.AnimalCount)
                    .ToList(),
                "address" => shelters
                    .OrderBy(s => s.Address)
                    .ToList(),
                "address_desc" => shelters
                    .OrderByDescending(s => s.Address)
                    .ToList(),
                "phoneNumber" => shelters
                    .OrderBy(s => s.PhoneNumber)
                    .ToList(),
                "phoneNumber_desc" => shelters
                    .OrderByDescending(s => s.PhoneNumber)
                    .ToList(),
                "dateAdded" => shelters
                    .OrderBy(s => s.DateAdded)
                    .ToList(),
                "dateAdded_desc" => shelters
                    .OrderByDescending(s => s.DateAdded)
                    .ToList(),
                _ => shelters
                    .OrderBy(s => s.Name)
                    .ToList(),
            };

            return shelters;
        }

        private static List<ShelterViewModel> FilterShelters(List<ShelterViewModel> shelters, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                shelters = shelters
                    .Where(animal => animal.Name
                    .ToLower()
                    .Contains(searchString
                    .ToLower()))
                    .ToList();
            }

            return shelters;
        }

        private void AddAnimalCount(List<Shelter> shelterList)
        {
            foreach (var shelter in shelterList)
            {
                shelter.AnimalCount = _animalRepository
                    .GetAllAnimals()
                    .Where(x => x.ShelterId == shelter.Id)
                    .Count();
            }
        }
    }
}