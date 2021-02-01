using WebPatversme.Models;
using System.Collections.Generic;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IShelterService
    {
        List<Shelters> ShelterList();

        void AddNewShelter(Shelters shelter);
    }
}