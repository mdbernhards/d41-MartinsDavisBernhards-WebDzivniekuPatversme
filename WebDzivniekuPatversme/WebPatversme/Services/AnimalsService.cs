using System;
using System.Linq;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

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

        public AnimalsViewModel ObjectForCreatingAnimal()
        {
            return new AnimalsViewModel
            {
                AnimalShelters = _shelterRepository.GetAllAnimalShelters()
            };
        }

        public List<Animals> GetAllAnimalList()
        {
            var AnimalList = _animalsRepository.GetAllAnimals();

            return AnimalList;
        }

        public Animals GetAnimalById(string Id)
        {
            var AnimalList = _animalsRepository.GetAllAnimals();
            var animal = AnimalList.Where(animal => animal.AnimalID == Id).FirstOrDefault();

            return animal;
        }

        public void DeleteAnimals(Animals animal)
        {
            _animalsRepository.DeleteAnimal(animal);
        }

        public void AddNewAnimal(Animals animal)
        {
            animal.AnimalID = Guid.NewGuid().ToString();
            animal.DateAdded = DateTime.Now;

            _animalsRepository.CreateNewAnimal(animal);
        }
    }
}