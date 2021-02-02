using System;
using WebDzivniekuPatversme.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebDzivniekuPatversme.Repository.Interfaces;

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

            using (MySqlConnection conn = _dbcontext.GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand("select * from AnimalShelters", conn);
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
            }
            return list;
        }

        public void CreateNewAnimalShelter(Shelters newAnimalShelters)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("INSERT INTO AnimalShelters (ID, AnimalCapacity, Name, Address, PhoneNumber, ImagePath) " +
                                                "VALUES (\"" + newAnimalShelters.AnimalShelterID + "\", " + newAnimalShelters.AnimalCapacity + ", \"" + newAnimalShelters.Name + "\", \""
                                                            + newAnimalShelters.Address + "\", \"" + newAnimalShelters.PhoneNumber + "\", \"" + newAnimalShelters.ImagePath + "\")", conn);

            var reader = cmd.ExecuteReader();
        }

        public void DeleteShelters(Shelters shelters)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            conn.Open();

            string sqlQuerry = "Delete from AnimalShelters where ID = \"" + shelters.AnimalShelterID + "\";";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            var reader = cmd.ExecuteReader();
        }
    }
}