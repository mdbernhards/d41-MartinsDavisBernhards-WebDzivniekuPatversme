using System.Collections.Generic;
using WebDzivniekuPatversme.Models;

namespace WebDzivniekuPatversme.Repositories.Interfaces
{
    public interface IAnimalsRepository
    {
        List<Animals> GetAllAnimals();

        void CreateNewAnimal(Animals newAnimal);

        void DeleteAnimal(Animals animal);

        void EditAnimal(Animals newAnimal);

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