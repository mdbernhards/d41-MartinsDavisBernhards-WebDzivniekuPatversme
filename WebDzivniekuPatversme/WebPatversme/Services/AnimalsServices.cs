using WebPatversme.Models;
using System.Collections.Generic;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebDzivniekuPatversme.Services
{
    public class AnimalsServices : IAnimalsServices
    {
        private readonly IAnimalsRepository _animalsRepository;

        public AnimalsServices(
            IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }

        public List<Animals> AnimalsTable()
        {
            return _animalsRepository.GetAllAnimals();
        }

        public void AddNewAnimal(Animals animal)
        {
            _animalsRepository.CreateNewAnimal(animal);
        }
    }
}