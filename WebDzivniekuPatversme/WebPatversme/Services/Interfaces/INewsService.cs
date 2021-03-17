using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.News;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface INewsService
    {
        List<News> GetAllNews();

        void AddNewNews(News news);

        News GetNewsById(string Id);

        void DeleteNews(News news);

        void EditNews(News news);

        List<NewsViewModel> FilterAndSortNews(List<NewsViewModel> news, string sortOrder, string searchString);
    }
}