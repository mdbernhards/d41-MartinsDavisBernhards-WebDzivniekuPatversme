using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Shelters;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IShelterService
    {
        List<Shelter> GetAllShelters();

        void AddNewShelter(Shelter shelter);

        Shelter GetShelterById(string Id);

        void DeleteShelter(Shelter shelter);

        void EditShelter(Shelter shelter);

        List<ShelterViewModel> FilterAndSortShelters(List<ShelterViewModel> shelter, string sortOrder, string searchString);
    }
}