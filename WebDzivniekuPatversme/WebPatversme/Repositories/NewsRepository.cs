using System;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Repository.Interfaces;
using MySql.Data.MySqlClient;

namespace WebDzivniekuPatversme.Repository
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
                    DateCreated = Convert.ToDateTime(reader["DateCreated"]),
                    Text = Convert.ToString(reader["Text"]),
                    ImagePath = Convert.ToString(reader["ImagePath"])
                });
            }
            return list;
        }

        public void CreateNewNews(News newNews)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "INSERT INTO News (ID, DateCreated, Text, ImagePath) VALUES (@id, @dateCreated, @text, @imagePath);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string dateCreatedString = newNews.DateCreated.ToString("yyyy-MM-dd");

            cmd.Parameters.AddWithValue("@id", newNews.NewsID);
            cmd.Parameters.AddWithValue("@dateCreated", dateCreatedString);
            cmd.Parameters.AddWithValue("@text", newNews.Text);
            cmd.Parameters.AddWithValue("@imagePath", newNews.ImagePath);

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
            var sqlQuerry = "UPDATE News SET DateCreated = @dateCreated, Text = @text, ImagePath = @imagePath WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string dateCreatedString = news.DateCreated.ToString("yyyy-MM-dd");

            cmd.Parameters.AddWithValue("@dateCreated", dateCreatedString);
            cmd.Parameters.AddWithValue("@text", news.Text);
            cmd.Parameters.AddWithValue("@imagePath", news.ImagePath);
            cmd.Parameters.AddWithValue("@id", news.NewsID);

            var reader = cmd.ExecuteReader();
        }
    }
}