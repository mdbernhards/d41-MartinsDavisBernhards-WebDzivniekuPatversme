using WebPatversme.Models;
using System.Collections.Generic;

namespace WebDzivniekuPatversme.Repository.Interfaces
{
    public interface INewsRepository
    {
        List<News> GetAllNews();

        void CreateNewNews(News newNews);
    }
}