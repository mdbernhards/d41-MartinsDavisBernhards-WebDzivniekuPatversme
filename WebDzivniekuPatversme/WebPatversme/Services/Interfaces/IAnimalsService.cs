using WebPatversme.Models;
using System.Collections.Generic;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IAnimalsService
    {
        List<Animals> AnimalsTable();

        void AddNewAnimal(Animals animal);
    }
}