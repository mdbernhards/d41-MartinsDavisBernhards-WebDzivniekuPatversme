using System.Collections.Generic;
using WebPatversme.Models;

namespace WebDzivniekuPatversme.Repository.Interfaces
{
    public interface IAnimalsRepository
    {
        List<Animals> GetAllAnimals();
    }
}