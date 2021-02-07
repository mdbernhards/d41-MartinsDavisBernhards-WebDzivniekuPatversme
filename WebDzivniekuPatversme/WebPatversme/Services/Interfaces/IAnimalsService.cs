using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IAnimalsService
    {
        List<Animals> GetAllAnimalList();

        void AddNewAnimal(Animals animal);

        void EditAnimal(Animals animal);

        List<Shelters> GetAllShelters();

        List<AnimalsViewModel> AddAnimalShelterNames(List<AnimalsViewModel> animals);

        List<AnimalsViewModel> FilterAndSortAnimals(List<AnimalsViewModel> animals, string sortOrder, string searchString);

        Animals GetAnimalById(string Id);

        void DeleteAnimal(Animals animal);
    }
}