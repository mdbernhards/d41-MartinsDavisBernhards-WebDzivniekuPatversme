using System;
using System.Linq;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repositories.Interfaces;

namespace WebDzivniekuPatversme.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IAnimalsRepository _animalsRepository;
        private readonly IShelterRepository _shelterRepository;

        public AnimalsService(
            IAnimalsRepository animalsRepository,
            IShelterRepository shelterRepository)
        {
            _animalsRepository = animalsRepository;
            _shelterRepository = shelterRepository;
        }

        public List<Shelters> GetAllShelters()
        {
            return _shelterRepository.GetAllAnimalShelters();
        }

        public List<Animals> GetAllAnimalList()
        {
            var AnimalList = _animalsRepository.GetAllAnimals();

            return AnimalList;
        }

        public List<AnimalsViewModel> AddAnimalShelterNames(List<AnimalsViewModel> animals)
        {
            var ShelterList = GetAllShelters();

            foreach (var animal in animals)
            {
                var shelter = ShelterList.Where(x => x.AnimalShelterID == animal.AnimalShelterId).FirstOrDefault();
                animal.AnimalShelterName = shelter.Name;
            }

            return animals;
        }

        public List<AnimalsViewModel> SortAnimals(List<AnimalsViewModel> animals, string sortOrder)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    animals = animals.OrderByDescending(s => s.Name).ToList();
                    break;
                case "age":
                    animals = animals.OrderBy(s => s.Age).ToList();
                    break;
                case "age_desc":
                    animals = animals.OrderByDescending(s => s.Age).ToList();
                    break;
                case "species":
                    animals = animals.OrderBy(s => s.Species).ToList();
                    break;
                case "species_desc":
                    animals = animals.OrderByDescending(s => s.Species).ToList();
                    break;
                case "weight":
                    animals = animals.OrderBy(s => s.Weight).ToList();
                    break;
                case "weight_desc":
                    animals = animals.OrderByDescending(s => s.Weight).ToList();
                    break;
                case "shelter":
                    animals = animals.OrderBy(s => s.AnimalShelterName).ToList();
                    break;
                case "shelter_desc":
                    animals = animals.OrderByDescending(s => s.AnimalShelterName).ToList();
                    break;
                case "dateAdded":
                    animals = animals.OrderBy(s => s.DateAdded).ToList();
                    break;
                case "dateadded_desc":
                    animals = animals.OrderByDescending(s => s.DateAdded).ToList();
                    break;
                case "colour":
                    animals = animals.OrderBy(s => s.Colour).ToList();
                    break;
                case "colour_desc":
                    animals = animals.OrderByDescending(s => s.Colour).ToList();
                    break;
                default:
                    animals = animals.OrderBy(s => s.Name).ToList();
                    break;
            }
            return animals;
        }

        public Animals GetAnimalById(string Id)
        {
            var AnimalList = _animalsRepository.GetAllAnimals();
            var animal = AnimalList.Where(animal => animal.AnimalID == Id).FirstOrDefault();

            return animal;
        }

        public void DeleteAnimal(Animals animal)
        {
            _animalsRepository.DeleteAnimal(animal);
        }

        public void AddNewAnimal(Animals animal)
        {
            animal.AnimalID = Guid.NewGuid().ToString();
            animal.DateAdded = DateTime.Now;

            _animalsRepository.CreateNewAnimal(animal);
        }

        public void EditAnimal(Animals animal)
        {
            _animalsRepository.EditAnimal(animal);
        }
    }
}