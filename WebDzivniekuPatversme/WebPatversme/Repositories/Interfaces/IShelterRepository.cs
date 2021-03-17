using System.Collections.Generic;
using WebDzivniekuPatversme.Models;

namespace WebDzivniekuPatversme.Repositories.Interfaces
{
    public interface IShelterRepository
    {
        List<Shelter> GetAllShelters();

        void CreateNewShelter(Shelter newAnimalShelters);

        void DeleteShelter(Shelter shelters);

        void EditShelter(Shelter shelter);
    }
}