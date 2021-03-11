using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Models.ViewModels.Animal;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IAnimalsService
    {
        List<Animal> GetAllAnimalList();

        void AddNewAnimal(Animal animal);

        void EditAnimal(Animal animal);

        List<Shelter> GetAllShelters();

        List<AnimalColour> GetAllColours();

        List<AnimalSpecies> GetAllSpecies();

        List<AnimalSpeciesType> GetAllSpeciesTypes();

        List<AnimalViewModel> AddAnimalShelterNames(List<AnimalViewModel> animals);

        AnimalViewModel AddAnimalShelterNames(AnimalViewModel animal);

        List<AnimalViewModel> FilterAndSortAnimals(List<AnimalViewModel> animals, string sortOrder, AnimalFilter filter);

        Animal GetAnimalById(string Id);

        void DeleteAnimal(Animal animal);

        void SendAnimalEmail(Animal animal);

        AnimalFilter CreateAnimalFilter(string name, string age, string species, string speciesType, string colour, string shelter);

        DropDownItemListViewModel CreateAnimalDropDownListValues(List<AnimalViewModel> animalList, AnimalFilter filter);
    }
}