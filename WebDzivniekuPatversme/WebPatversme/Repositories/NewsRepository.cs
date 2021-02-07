using System;
using System.Collections.Generic;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Repositories.Interfaces;
using MySql.Data.MySqlClient;

namespace WebDzivniekuPatversme.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly WebShelterDbContext _dbcontext;

        public NewsRepository(
            WebShelterDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public List<News> GetAllNews()
        {
            List<News> list = new List<News>();

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM News;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new News()
                {
                    NewsID = Convert.ToString(reader["ID"]),
                    DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                    Title = Convert.ToString(reader["Title"]),
                    Text = Convert.ToString(reader["Text"]),
                    ImagePath = Convert.ToString(reader["ImagePath"])
                });
            }
            return list;
        }

        public void CreateNewNews(News newNews)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "INSERT INTO News (ID, DateAdded, Text, ImagePath, Title, UserId) VALUES (@id, @dateAdded, @text, @imagePath, @title, @userId);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string dateAddedString = newNews.DateAdded.ToString("yyyy-MM-dd HH:MM:ss");

            cmd.Parameters.AddWithValue("@id", newNews.NewsID);
            cmd.Parameters.AddWithValue("@dateAdded", dateAddedString);
            cmd.Parameters.AddWithValue("@text", newNews.Text);
            cmd.Parameters.AddWithValue("@imagePath", newNews.ImagePath);
            cmd.Parameters.AddWithValue("@title", newNews.Title);
            cmd.Parameters.AddWithValue("@userId", newNews.UserID);

            var reader = cmd.ExecuteReader();
        }

        public void DeleteNews(News news)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "DELETE FROM news WHERE ID = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", news.NewsID);

            var reader = cmd.ExecuteReader();
        }

        public void EditNews(News news)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "UPDATE News SET Text = @text, ImagePath = @imagePath, Title = @title WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@text", news.Text);
            cmd.Parameters.AddWithValue("@imagePath", news.ImagePath);
            cmd.Parameters.AddWithValue("@id", news.NewsID);
            cmd.Parameters.AddWithValue("@title", news.Title);

            var reader = cmd.ExecuteReader();
        }
    }
}