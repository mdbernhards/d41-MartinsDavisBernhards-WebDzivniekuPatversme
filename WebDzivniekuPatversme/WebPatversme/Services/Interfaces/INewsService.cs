using WebPatversme.Models;
using System.Collections.Generic;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface INewsService
    {
        List<News> NewsTable();

        void AddNewNews(News news);
    }
}