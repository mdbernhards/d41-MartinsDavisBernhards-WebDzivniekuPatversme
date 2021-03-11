using System.Collections.Generic;
using WebDzivniekuPatversme.Models;

namespace WebDzivniekuPatversme.Repositories.Interfaces
{
    public interface IAnimalsRepository
    {
        List<Animal> GetAllAnimals();

        void CreateNewAnimal(Animal newAnimal);

        void DeleteAnimal(Animal animal);

        void EditAnimal(Animal newAnimal);

        void CreateNewColour(AnimalColour colour);

        List<AnimalColour> GetAllColours();

        void DeleteColour(AnimalColour colour);

        void CreateNewSpecies(AnimalSpecies species);

        List<AnimalSpecies> GetAllSpecies();

        void DeleteSpecies(AnimalSpecies species);

        void CreateNewSpeciesType(AnimalSpeciesType speciesType);

        List<AnimalSpeciesType> GetAllSpeciesTypes();

        void DeleteSpeciesType(AnimalSpeciesType speciesType);
    }
}