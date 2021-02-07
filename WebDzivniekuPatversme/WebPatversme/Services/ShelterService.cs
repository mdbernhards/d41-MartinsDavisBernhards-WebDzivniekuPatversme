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

        public List<SheltersViewModel> SortShelters(List<SheltersViewModel> shelter, string sortOrder)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    shelter = shelter.OrderByDescending(s => s.Name).ToList();
                    break;
                case "capacity":
                    shelter = shelter.OrderBy(s => s.AnimalCapacity).ToList();
                    break;
                case "capacity_desc":
                    shelter = shelter.OrderByDescending(s => s.AnimalCapacity).ToList();
                    break;
                case "address":
                    shelter = shelter.OrderBy(s => s.Address).ToList();
                    break;
                case "address_desc":
                    shelter = shelter.OrderByDescending(s => s.Address).ToList();
                    break;
                case "phoneNumber":
                    shelter = shelter.OrderBy(s => s.PhoneNumber).ToList();
                    break;
                case "phoneNumber_desc":
                    shelter = shelter.OrderByDescending(s => s.PhoneNumber).ToList();
                    break;
                case "dateAdded":
                    shelter = shelter.OrderBy(s => s.DateAdded).ToList();
                    break;
                case "dateAdded_desc":
                    shelter = shelter.OrderByDescending(s => s.DateAdded).ToList();
                    break;
                default:
                    shelter = shelter.OrderBy(s => s.Name).ToList();
                    break;
            }
            return shelter;
        }
    }
}