using System.Collections.Generic;
using WebDzivniekuPatversme.Models;

namespace WebDzivniekuPatversme.Repositories.Interfaces
{
    public interface IShelterRepository
    {
        List<Shelter> GetAllAnimalShelters();

        void CreateNewAnimalShelter(Shelter newAnimalShelters);

        void DeleteShelters(Shelter shelters);

        void EditShelter(Shelter shelter);
    }
}