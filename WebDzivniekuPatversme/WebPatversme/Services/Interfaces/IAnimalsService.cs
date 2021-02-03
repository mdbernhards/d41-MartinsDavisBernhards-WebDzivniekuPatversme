using WebDzivniekuPatversme.Models;
using System.Collections.Generic;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IAnimalsService
    {
        List<Animals> GetAllAnimalList();

        void AddNewAnimal(Animals animal);

        void EditAnimal(Animals animal);

        List<Shelters> GetAllShelters();

        Animals GetAnimalById(string Id);

        void DeleteAnimals(Animals animal);
    }
}