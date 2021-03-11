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
    public class ShelterRepository : IShelterRepository
    {
        private readonly WebShelterDbContext _dbcontext;
        private readonly IWebHostEnvironment _appEnvironment;

        public ShelterRepository(
            WebShelterDbContext dbContext,
            IWebHostEnvironment appEnvironment)
        {
            _dbcontext = dbContext;
            _appEnvironment = appEnvironment;
        }

        public List<Shelter> GetAllAnimalShelters()
        {
            List<Shelter> list = new List<Shelter>();

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM Shelters;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Shelter()
                {
                    Id = Convert.ToString(reader["Id"]),
                    AnimalCapacity = Convert.ToInt32(reader["AnimalCapacity"]),
                    Name = Convert.ToString(reader["Name"]),
                    Address = Convert.ToString(reader["Address"]),
                    PhoneNumber = Convert.ToString(reader["PhoneNumber"]),
                    ImagePath = Convert.ToString(reader["ImagePath"]),
                    DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                    Email = Convert.ToString(reader["Email"]),
                    Description = Convert.ToString(reader["Description"]),
                });
            }
            return list;
        }

        public void CreateNewAnimalShelter(Shelter shelter)
        {
            shelter.ImagePath = SaveImage(shelter);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO Shelters (Id, AnimalCapacity, Name, Address, PhoneNumber, ImagePath, DateAdded, Email, Description) VALUES (@id, @animalCapacity, @name, @adress, @phoneNumber, @imagePath, @dateAdded, @email, @description);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string dateAddedString = shelter.DateAdded.ToString("yyyy-MM-dd HH:MM:ss");

            cmd.Parameters.AddWithValue("@id", shelter.Id);
            cmd.Parameters.AddWithValue("@animalCapacity", shelter.AnimalCapacity);
            cmd.Parameters.AddWithValue("@name", shelter.Name);
            cmd.Parameters.AddWithValue("@adress", shelter.Address);
            cmd.Parameters.AddWithValue("@phoneNumber", shelter.PhoneNumber);
            cmd.Parameters.AddWithValue("@imagePath", shelter.ImagePath);
            cmd.Parameters.AddWithValue("@dateAdded", dateAddedString);
            cmd.Parameters.AddWithValue("@email", shelter.Email);
            cmd.Parameters.AddWithValue("@description", shelter.Description);

            var reader = cmd.ExecuteReader();
        }

        public void DeleteShelters(Shelter shelter)
        {
            DeleteAllSheltersAnimals(shelter);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "DELETE FROM Shelters WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", shelter.Id);

            var reader = cmd.ExecuteReader();
        }

        private void DeleteAllSheltersAnimals(Shelter shelter)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "DELETE FROM Animals WHERE ShelterId = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", shelter.Id);

            var reader = cmd.ExecuteReader();

        }

        public void EditShelter(Shelter shelter)
        {
            shelter.ImagePath = SaveImage(shelter);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "UPDATE Shelters SET AnimalCapacity = @animalCapacity, Name = @name, Address =  @adress, PhoneNumber = @phoneNumber, ImagePath = @imagePath, Email = @email, Description = @description WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@animalCapacity", shelter.AnimalCapacity);
            cmd.Parameters.AddWithValue("@name", shelter.Name);
            cmd.Parameters.AddWithValue("@adress", shelter.Address);
            cmd.Parameters.AddWithValue("@phoneNumber", shelter.PhoneNumber);
            cmd.Parameters.AddWithValue("@imagePath", shelter.ImagePath);
            cmd.Parameters.AddWithValue("@id", shelter.Id);
            cmd.Parameters.AddWithValue("@email", shelter.Email);
            cmd.Parameters.AddWithValue("@description", shelter.Description);

            var reader = cmd.ExecuteReader();
        }

        private string SaveImage(Shelter shelters)
        {
            var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\images\\shelters");

            if (shelters.Image != null && shelters.Image.Length > 0)
            {
                var fileName = Path.GetFileName(shelters.Name + shelters.Id + Path.GetExtension(shelters.Image.FileName));

                File.Delete(Path.Combine(uploads, fileName));

                var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
                shelters.Image.CopyTo(fileStream);
                fileStream.Close();

                return fileName;
            }

            return string.Empty;
        }
    }
}