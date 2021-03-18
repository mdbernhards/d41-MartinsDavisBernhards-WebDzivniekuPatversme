using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Models.ViewModels.Animal;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IAnimalsService
    {
        List<Animal> GetAllAnimals();

        void AddNewAnimal(Animal animal);

        void EditAnimal(Animal animal);

        List<Shelter> GetAllShelters();

        List<AnimalColourViewModel> GetAllColours();

        List<AnimalSpeciesViewModel> GetAllSpecies();

        List<AnimalSpeciesTypeViewModel> GetAllSpeciesTypes();

        List<AnimalViewModel> AddShelterNames(List<AnimalViewModel> animals);

        string GetShelterName(string id);

        List<AnimalViewModel> FilterAndSortAnimals(List<AnimalViewModel> animals, string sortOrder, AnimalFilter filter);

        Animal GetAnimalById(string Id);

        void DeleteAnimal(Animal animal);

        void SendAnimalEmail(Animal animal);

        AnimalFilter CreateAnimalFilter(string name, string age, string species, string speciesType, string colour, string shelter);

        DropDownItemListViewModel CreateAnimalDropDownListValues(List<AnimalViewModel> animalList, AnimalFilter filter);
    }
}