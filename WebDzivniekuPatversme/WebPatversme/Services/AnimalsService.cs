using System;
using WebPatversme.Models;
using System.Collections.Generic;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;
using WebPatversme.Models.ViewModels;

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

        public List<Animals> AnimalList()
        {
            var AnimalList = _animalsRepository.GetAllAnimals();

            return _animalsRepository.GetAllAnimals();
        }

        public void AddNewAnimal(Animals animal)
        {
            animal.AnimalID = Guid.NewGuid().ToString();
            animal.DateAdded = DateTime.Now;

            _animalsRepository.CreateNewAnimal(animal);
        }
    }
}