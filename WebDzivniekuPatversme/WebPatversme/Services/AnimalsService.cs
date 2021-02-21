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
            foreach (var animal in animals)
            {
                AddAnimalShelterNames(animal);
            }

            return animals;
        }

        public AnimalsViewModel AddAnimalShelterNames(AnimalsViewModel animal)
        {
            var ShelterList = GetAllShelters();

            var shelter = ShelterList.Where(x => x.AnimalShelterID == animal.AnimalShelterId).FirstOrDefault();
            animal.AnimalShelterName = shelter.Name;

            return animal;
        }

        public List<AnimalsViewModel> FilterAndSortAnimals(List<AnimalsViewModel> animals, string sortOrder, AnimalFilter filter)
        {
            animals = FilterAnimals(animals, filter);
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

        private List<AnimalsViewModel> FilterAnimals(List<AnimalsViewModel> animals, AnimalFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                animals = animals.Where(animal => animal.Name.Contains(filter.Name)).ToList();
            }

            if(filter.Age != 0)
            {
                animals = animals.Where(animal => animal.Age == filter.Age).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Species))
            {
                animals = animals.Where(animal => animal.Species.Contains(filter.Species)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Colour))
            {
                animals = animals.Where(animal => animal.Colour.Contains(filter.Colour)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Shelter))
            {
                animals = animals.Where(animal => animal.AnimalShelterName.Contains(filter.Shelter)).ToList();
            }

            if (filter.Weight != 0)
            {
                animals = animals.Where(animal => animal.Weight == filter.Weight).ToList();
            }

            return animals;
        }
    }
}