using System;
using System.Collections.Generic;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Repositories.Interfaces;
using MySql.Data.MySqlClient;

namespace WebDzivniekuPatversme.Repositories
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly WebShelterDbContext _dbcontext;

        public ShelterRepository(
            WebShelterDbContext dbContext)
        {
            _dbcontext = dbContext;
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
                    DateAdded = Convert.ToDateTime(reader["DateAdded"])
                });
            }
            return list;
        }

        public void CreateNewAnimalShelter(Shelters newAnimalShelters)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO AnimalShelters (ID, AnimalCapacity, Name, Address, PhoneNumber, ImagePath, DateAdded) VALUES (@id, @animalCapacity, @name, @adress, @phoneNumber, @imagePath, @dateAdded);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string dateAddedString = newAnimalShelters.DateAdded.ToString("yyyy-MM-dd");

            cmd.Parameters.AddWithValue("@id", newAnimalShelters.AnimalShelterID);
            cmd.Parameters.AddWithValue("@animalCapacity", newAnimalShelters.AnimalCapacity);
            cmd.Parameters.AddWithValue("@name", newAnimalShelters.Name);
            cmd.Parameters.AddWithValue("@adress", newAnimalShelters.Address);
            cmd.Parameters.AddWithValue("@phoneNumber", newAnimalShelters.PhoneNumber);
            cmd.Parameters.AddWithValue("@imagePath", newAnimalShelters.ImagePath);
            cmd.Parameters.AddWithValue("@dateAdded", dateAddedString);

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
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "UPDATE AnimalShelters SET AnimalCapacity = @animalCapacity, Name = @name, Address =  @adress, PhoneNumber = @phoneNumber, ImagePath = @imagePath WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@animalCapacity", shelter.AnimalCapacity);
            cmd.Parameters.AddWithValue("@name", shelter.Name);
            cmd.Parameters.AddWithValue("@adress", shelter.Address);
            cmd.Parameters.AddWithValue("@phoneNumber", shelter.PhoneNumber);
            cmd.Parameters.AddWithValue("@imagePath", shelter.ImagePath);
            cmd.Parameters.AddWithValue("@id", shelter.AnimalShelterID);

            var reader = cmd.ExecuteReader();
        }
    }
}