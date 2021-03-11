using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Shelter;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IShelterService
    {
        List<Shelter> GetAllShelterList();

        void AddNewShelter(Shelter shelter);

        Shelter GetShelterById(string Id);

        void DeleteShelter(Shelter shelter);

        void EditShelter(Shelter shelter);

        List<ShelterViewModel> SortShelters(List<ShelterViewModel> shelter, string sortOrder, string searchString);
    }
}