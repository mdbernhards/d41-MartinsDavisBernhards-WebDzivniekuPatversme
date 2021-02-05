using System.Collections.Generic;
using WebDzivniekuPatversme.Models;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface INewsService
    {
        List<News> GetAllNewsList();

        void AddNewNews(News news);

        News GetNewsById(string Id);

        void DeleteNews(News news);

        void EditNews(News news);
    }
}