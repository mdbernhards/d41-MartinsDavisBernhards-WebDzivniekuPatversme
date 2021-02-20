using System;
using System.IO;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Repositories.Interfaces;

namespace WebDzivniekuPatversme.Repositories
{
    public class AnimalsRepository : IAnimalsRepository
    {
        private readonly WebShelterDbContext _dbcontext;
        private readonly IWebHostEnvironment _appEnvironment;

        public AnimalsRepository(
            WebShelterDbContext dbContext, 
            IWebHostEnvironment appEnvironment)
        {
            _dbcontext = dbContext;
            _appEnvironment = appEnvironment;
        }

        public List<Animals> GetAllAnimals()
        {
            List<Animals> list = new List<Animals>();

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM Animals;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Animals animal = new Animals()
                {
                    AnimalID = Convert.ToString(reader["ID"]),
                    Weight = Convert.ToInt32(reader["Weight"]),
                    BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                    DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                    About = Convert.ToString(reader["About"]),
                    Name = Convert.ToString(reader["Name"]),
                    Species = Convert.ToString(reader["Species"]),
                    Colour = Convert.ToString(reader["Colour"]),
                    ImagePath = Convert.ToString(reader["ImagePath"]),
                    AnimalShelterId = Convert.ToString(reader["AnimalShelterID"]),
                };

                animal.Age = CalculateAge(animal.BirthDate);

                list.Add(animal);
            }
            return list;
        }

        public void CreateNewAnimal(Animals animal)
        {
            animal.ImagePath = SaveImage(animal);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO Animals (ID, Weight, BirthDate, DateAdded, About, Name, Species, Colour, ImagePath, AnimalShelterID) " +
                                                "VALUES (@id, @weight, @birthDate, @dateAdded, @about, @name, @species, @colour, @imagePath, @animalShelterId);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string birthDateeString = animal.BirthDate.ToString("yyyy-MM-dd");
            string dateAddedString = animal.DateAdded.ToString("yyyy-MM-dd HH:MM:ss");

            cmd.Parameters.AddWithValue("@weight", animal.Weight);
            cmd.Parameters.AddWithValue("@birthDate", birthDateeString);
            cmd.Parameters.AddWithValue("@dateAdded", dateAddedString);
            cmd.Parameters.AddWithValue("@about", animal.About);
            cmd.Parameters.AddWithValue("@name", animal.Name);
            cmd.Parameters.AddWithValue("@species", animal.Species);
            cmd.Parameters.AddWithValue("@colour", animal.Colour);
            cmd.Parameters.AddWithValue("@imagePath", animal.ImagePath);
            cmd.Parameters.AddWithValue("@animalShelterID", animal.AnimalShelterId);
            cmd.Parameters.AddWithValue("@id", animal.AnimalID);

            var reader = cmd.ExecuteReader();
        }

        public void DeleteAnimal(Animals animal)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "DELETE FROM Animals WHERE ID = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", animal.AnimalID);

            var reader = cmd.ExecuteReader();
        }

        public void EditAnimal(Animals animal)
        {
            animal.ImagePath = SaveImage(animal);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "UPDATE Animals SET Weight = @weight , BirthDate =  @birthDate , About = @about ," +
                          " Name = @name , Species = @species , Colour = @colour , ImagePath = @imagePath , AnimalShelterID = @animalShelterID WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string birthDateeString = animal.BirthDate.ToString("yyyy-MM-dd");

            cmd.Parameters.AddWithValue("@weight", animal.Weight);
            cmd.Parameters.AddWithValue("@birthDate", birthDateeString);
            cmd.Parameters.AddWithValue("@about", animal.About);
            cmd.Parameters.AddWithValue("@name", animal.Name);
            cmd.Parameters.AddWithValue("@species", animal.Species);
            cmd.Parameters.AddWithValue("@colour", animal.Colour);
            cmd.Parameters.AddWithValue("@imagePath", animal.ImagePath);
            cmd.Parameters.AddWithValue("@animalShelterID", animal.AnimalShelterId);
            cmd.Parameters.AddWithValue("@id", animal.AnimalID);

            var reader = cmd.ExecuteReader();
        }

        private static int CalculateAge(DateTime birthDate)
        {
            var age = DateTime.Now.Year - birthDate.Year;

            if (birthDate > DateTime.Now.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        private string SaveImage(Animals animal)
        {
            var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\images\\animals");

            if (animal.Image != null && animal.Image.Length > 0)
            {
                var fileName = Path.GetFileName(animal.Name + animal.AnimalID + Path.GetExtension(animal.Image.FileName));

                File.Delete(Path.Combine(uploads, fileName));

                var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
                animal.Image.CopyTo(fileStream);
                fileStream.Close();

                return fileName;
            }

            return string.Empty;
        }
    }
}