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

        public List<Shelters> GetAllAnimalShelters()
        {
            List<Shelters> list = new List<Shelters>();

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM AnimalShelters;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Shelters()
                {
                    AnimalShelterID = Convert.ToString(reader["ID"]),
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

        public void CreateNewAnimalShelter(Shelters shelter)
        {
            shelter.ImagePath = SaveImage(shelter);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO AnimalShelters (ID, AnimalCapacity, Name, Address, PhoneNumber, ImagePath, DateAdded, Email, Description) VALUES (@id, @animalCapacity, @name, @adress, @phoneNumber, @imagePath, @dateAdded, @email, @description);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string dateAddedString = shelter.DateAdded.ToString("yyyy-MM-dd HH:MM:ss");

            cmd.Parameters.AddWithValue("@id", shelter.AnimalShelterID);
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

        public void DeleteShelters(Shelters shelter)
        {
            DeleteAllSheltersAnimals(shelter);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "DELETE FROM AnimalShelters WHERE ID = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", shelter.AnimalShelterID);

            var reader = cmd.ExecuteReader();
        }

        private void DeleteAllSheltersAnimals(Shelters shelter)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "DELETE FROM Animals WHERE AnimalShelterID = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", shelter.AnimalShelterID);

            var reader = cmd.ExecuteReader();

        }

        public void EditShelter(Shelters shelter)
        {
            shelter.ImagePath = SaveImage(shelter);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "UPDATE AnimalShelters SET AnimalCapacity = @animalCapacity, Name = @name, Address =  @adress, PhoneNumber = @phoneNumber, ImagePath = @imagePath, Email = @email, Description = @description WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@animalCapacity", shelter.AnimalCapacity);
            cmd.Parameters.AddWithValue("@name", shelter.Name);
            cmd.Parameters.AddWithValue("@adress", shelter.Address);
            cmd.Parameters.AddWithValue("@phoneNumber", shelter.PhoneNumber);
            cmd.Parameters.AddWithValue("@imagePath", shelter.ImagePath);
            cmd.Parameters.AddWithValue("@id", shelter.AnimalShelterID);
            cmd.Parameters.AddWithValue("@email", shelter.Email);
            cmd.Parameters.AddWithValue("@description", shelter.Description);

            var reader = cmd.ExecuteReader();
        }

        private string SaveImage(Shelters shelters)
        {
            var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\images\\shelters");

            if (shelters.Image != null && shelters.Image.Length > 0)
            {
                var fileName = Path.GetFileName(shelters.Name + shelters.AnimalShelterID + Path.GetExtension(shelters.Image.FileName));

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