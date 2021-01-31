using WebPatversme.Models;
using System.Collections.Generic;
using WebPatversme.Models.ViewModels;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IAnimalsService
    {
        List<Animals> AnimalList();

        void AddNewAnimal(Animals animal);

        AnimalsViewModel ObjectForCreatingAnimal();
    }
}