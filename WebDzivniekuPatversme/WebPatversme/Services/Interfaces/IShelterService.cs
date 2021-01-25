using WebPatversme.Models;
using System.Collections.Generic;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IShelterService
    {
        List<AnimalShelters> AnimalShelterTable();

        void AddNewShelter(AnimalShelters shelter);
    }
}