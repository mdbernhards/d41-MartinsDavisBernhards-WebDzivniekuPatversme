using System;
using System.IO;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Hosting;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Repositories.Interfaces;

namespace WebDzivniekuPatversme.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly WebShelterDbContext _dbcontext;
        private readonly IWebHostEnvironment _appEnvironment;

        public NewsRepository(
            WebShelterDbContext dbContext,
            IWebHostEnvironment appEnvironment)
        {
            _dbcontext = dbContext;
            _appEnvironment = appEnvironment;
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
                    Id = Convert.ToString(reader["Id"]),
                    DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                    Title = Convert.ToString(reader["Title"]),
                    Text = Convert.ToString(reader["Text"]),
                    ImagePath = Convert.ToString(reader["ImagePath"]),
                    UserId = Convert.ToString(reader["UserId"]),
                });
            }

            return list;
        }

        public void CreateNewNews(News news)
        {
            news.ImagePath = SaveImage(news);

            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "INSERT INTO News (Id, DateAdded, Text, ImagePath, Title, UserId) VALUES (@id, @dateAdded, @text, @imagePath, @title, @userId);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string dateAddedString = news.DateAdded.ToString("yyyy-MM-dd HH:MM:ss");

            cmd.Parameters.AddWithValue("@id", news.Id);
            cmd.Parameters.AddWithValue("@dateAdded", dateAddedString);
            cmd.Parameters.AddWithValue("@text", news.Text);
            cmd.Parameters.AddWithValue("@imagePath", news.ImagePath);
            cmd.Parameters.AddWithValue("@title", news.Title);
            cmd.Parameters.AddWithValue("@userId", news.UserId);

            var reader = cmd.ExecuteReader();
        }

        public void DeleteNews(News news)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "DELETE FROM news WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", news.Id);

            var reader = cmd.ExecuteReader();
        }

        public void EditNews(News news)
        {
            news.ImagePath = SaveImage(news);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "UPDATE News SET Text = @text, ImagePath = @imagePath, Title = @title WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@text", news.Text);
            cmd.Parameters.AddWithValue("@imagePath", news.ImagePath);
            cmd.Parameters.AddWithValue("@id", news.Id);
            cmd.Parameters.AddWithValue("@title", news.Title);

            var reader = cmd.ExecuteReader();
        }

        private string SaveImage(News news)
        {
            var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\images\\news");

            if (news.Image != null && news.Image.Length > 0)
            {
                var fileName = Path.GetFileName(news.Title + news.Id + Path.GetExtension(news.Image.FileName));

                File.Delete(Path.Combine(uploads, fileName));

                var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
                news.Image.CopyTo(fileStream);
                fileStream.Close();

                return fileName;
            }

            return string.Empty;
        }
    }
}