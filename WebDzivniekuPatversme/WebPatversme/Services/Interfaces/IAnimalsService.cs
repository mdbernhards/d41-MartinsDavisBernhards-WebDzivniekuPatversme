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

        AnimalsViewModel AddAnimalShelterNames(AnimalsViewModel animal);

        List<AnimalsViewModel> FilterAndSortAnimals(List<AnimalsViewModel> animals, string sortOrder, AnimalFilter filter);

        Animals GetAnimalById(string Id);

        void DeleteAnimal(Animals animal);

        void SendAnimalEmail(Animals animal);

        AnimalFilter CreateAnimalFilter(string name, string age, string species, string speciesType, string colour, string shelter);

        DropDownItemListViewModel CreateAnimalDropDownListValues(List<AnimalsViewModel> animalList, AnimalFilter filter);
    }
}