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

        public List<AnimalsViewModel> FilterAndSortAnimals(List<AnimalsViewModel> animals, string sortOrder, string searchString)
        {
            animals = FilterAnimals(animals, searchString);
            animals = OrderAnimals(animals, sortOrder);

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

        private List<AnimalsViewModel> OrderAnimals(List<AnimalsViewModel> animals, string sortOrder)
        {
            animals = sortOrder switch
            {
                "name_desc" => animals.OrderByDescending(s => s.Name).ToList(),
                "age" => animals.OrderBy(s => s.Age).ToList(),
                "age_desc" => animals.OrderByDescending(s => s.Age).ToList(),
                "species" => animals.OrderBy(s => s.Species).ToList(),
                "species_desc" => animals.OrderByDescending(s => s.Species).ToList(),
                "weight" => animals.OrderBy(s => s.Weight).ToList(),
                "weight_desc" => animals.OrderByDescending(s => s.Weight).ToList(),
                "shelter" => animals.OrderBy(s => s.AnimalShelterName).ToList(),
                "shelter_desc" => animals.OrderByDescending(s => s.AnimalShelterName).ToList(),
                "dateAdded" => animals.OrderBy(s => s.DateAdded).ToList(),
                "dateadded_desc" => animals.OrderByDescending(s => s.DateAdded).ToList(),
                "colour" => animals.OrderBy(s => s.Colour).ToList(),
                "colour_desc" => animals.OrderByDescending(s => s.Colour).ToList(),
                _ => animals.OrderBy(s => s.Name).ToList(),
            };
            return animals;
        }

        private List<AnimalsViewModel> FilterAnimals(List<AnimalsViewModel> animals, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                animals = animals.Where(animal => animal.Name.Contains(searchString)).ToList();
            }

            return animals;
        }
    }
}