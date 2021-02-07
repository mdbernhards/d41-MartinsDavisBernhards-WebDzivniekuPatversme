using System.Collections.Generic;
using WebDzivniekuPatversme.Models;

namespace WebDzivniekuPatversme.Repositories.Interfaces
{
    public interface INewsRepository
    {
        List<News> GetAllNews();

        void CreateNewNews(News newNews);

        void DeleteNews(News news);

        void EditNews(News news);
    }
}