using WebDzivniekuPatversme.Models;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models.ViewModels;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IAnimalsService
    {
        List<Animals> GetAllAnimalList();

        void AddNewAnimal(Animals animal);

        AnimalsViewModel ObjectForCreatingAnimal();

        Animals GetAnimalById(string Id);

        void DeleteAnimals(Animals animal);
    }
}