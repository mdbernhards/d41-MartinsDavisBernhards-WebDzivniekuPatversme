using System.Collections.Generic;
using WebDzivniekuPatversme.Models;

namespace WebDzivniekuPatversme.Repository.Interfaces
{
    public interface IAnimalsRepository
    {
        List<Animals> GetAllAnimals();

        void CreateNewAnimal(Animals newAnimal);

        void DeleteAnimal(Animals animal);

        void EditAnimal(Animals newAnimal);
    }
}