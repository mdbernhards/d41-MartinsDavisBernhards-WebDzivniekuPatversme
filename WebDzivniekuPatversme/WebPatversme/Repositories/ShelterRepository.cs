using System;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Repository.Interfaces;
using MySql.Data.MySqlClient;

namespace WebDzivniekuPatversme.Repository
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
                });
            }
            return list;
        }

        public void CreateNewAnimalShelter(Shelters newAnimalShelters)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO AnimalShelters (ID, AnimalCapacity, Name, Address, PhoneNumber, ImagePath) VALUES (@id, @animalCapacity, @name, @adress, @phoneNumber, @imagePath);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", newAnimalShelters.AnimalShelterID);
            cmd.Parameters.AddWithValue("@animalCapacity", newAnimalShelters.AnimalCapacity);
            cmd.Parameters.AddWithValue("@name", newAnimalShelters.Name);
            cmd.Parameters.AddWithValue("@adress", newAnimalShelters.Address);
            cmd.Parameters.AddWithValue("@phoneNumber", newAnimalShelters.PhoneNumber);
            cmd.Parameters.AddWithValue("@imagePath", newAnimalShelters.ImagePath);

            var reader = cmd.ExecuteReader();
        }

        public void DeleteShelters(Shelters shelters)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "DELETE FROM AnimalShelters WHERE ID = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", shelters.AnimalShelterID);

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