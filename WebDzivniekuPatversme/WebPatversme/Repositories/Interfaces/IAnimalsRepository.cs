using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Animal;

namespace WebDzivniekuPatversme.Repositories.Interfaces
{
    public interface IAnimalsRepository
    {
        List<Animal> GetAllAnimals();

        void CreateNewAnimal(Animal newAnimal);

        void DeleteAnimal(Animal animal);

        void EditAnimal(Animal newAnimal);

        void CreateNewColour(AnimalColourViewModel colour);

        List<AnimalColourViewModel> GetAllColours();

        void DeleteColour(AnimalColourViewModel colour);

        void CreateNewSpecies(AnimalSpeciesViewModel species);

        List<AnimalSpeciesViewModel> GetAllSpecies();

        void DeleteSpecies(AnimalSpeciesViewModel species);

        void CreateNewSpeciesType(AnimalSpeciesTypeViewModel speciesType);

        List<AnimalSpeciesTypeViewModel> GetAllSpeciesTypes();

        void DeleteSpeciesType(AnimalSpeciesTypeViewModel speciesType);
    }
}