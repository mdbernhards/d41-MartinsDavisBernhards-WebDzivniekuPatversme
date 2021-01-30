using WebPatversme.Models;
using System.Collections.Generic;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IShelterService
    {
        List<Shelters> AnimalShelterTable();

        void AddNewShelter(Shelters shelter);
    }
}