using System;
using System.Linq;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repositories.Interfaces;
using WebDzivniekuPatversme.Models.ViewModels.News;
using Microsoft.AspNetCore.Identity;

namespace WebDzivniekuPatversme.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public NewsService(
            INewsRepository newsRepository,
            UserManager<ApplicationUser> userManager)
        {
            _newsRepository = newsRepository;
            _userManager = userManager;
        }

        public List<News> GetAllNews()
        {
            var newsList = _newsRepository.GetAllNews();

            GetUserFullNames(newsList);

            return newsList;
        }

        public News GetNewsById(string Id)
        {
            var newsList = GetAllNews();
            var news = newsList
                .Where(animal => animal.Id == Id).FirstOrDefault();

            return news;
        }

        public void DeleteNews(News news)
        {
            _newsRepository.DeleteNews(news);
        }

        public void AddNewNews(News news)
        {
            news.DateAdded = DateTime.Now;
            news.Id = Guid
                .NewGuid()
                .ToString();

            _newsRepository.CreateNewNews(news);
        }

        public void EditNews(News news)
        {
            _newsRepository.EditNews(news);
        }

        public List<NewsViewModel> FilterAndSortNews(List<NewsViewModel> news, string sortOrder, string searchString)
        {
            news = FilterNews(news, searchString);
            news = OrderNews(news, sortOrder);

            return news;
        }

        private void GetUserFullNames(List<News> newsList)
        {
            var users = _userManager.Users.ToList();

            foreach (var news in newsList)
            {
                var user = users.Where(x => x.Id == news.UserId).FirstOrDefault();

                news.UsersName = user.Name + " " + user.Surname;
            }
        }

        private static List<NewsViewModel> OrderNews(List<NewsViewModel> news, string sortOrder)
        {
            news = sortOrder switch
            {
                "title_desc" => news
                    .OrderByDescending(s => s.Title)
                    .ToList(),
                "text" => news
                    .OrderBy(s => s.Text)
                    .ToList(),
                "text_desc" => news
                    .OrderByDescending(s => s.Text)
                    .ToList(),
                "dateAdded" => news
                    .OrderBy(s => s.DateAdded)
                    .ToList(),
                "dateAdded_desc" => news
                    .OrderByDescending(s => s.DateAdded)
                    .ToList(),
                "usersName" => news
                    .OrderBy(s => s.UsersName)
                    .ToList(),
                "usersName_desc" => news
                    .OrderByDescending(s => s.UsersName)
                    .ToList(),
                _ => news
                    .OrderBy(s => s.Title)
                    .ToList(),
            };

            return news;
        }

        private static List<NewsViewModel> FilterNews(List<NewsViewModel> news, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                news = news
                    .Where(animal => animal.Title
                    .ToLower()
                    .Contains(searchString
                    .ToLower()))
                    .ToList();
            }

            return news;
        }
    }
}