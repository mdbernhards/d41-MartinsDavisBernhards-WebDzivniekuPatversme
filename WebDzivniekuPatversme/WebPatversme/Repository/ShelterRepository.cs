using System;
using WebPatversme.Models;
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

        public List<AnimalShelters> GetAllAnimalShelters()
        {
            List<AnimalShelters> list = new List<AnimalShelters>();

            using (MySqlConnection conn = _dbcontext.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from AnimalShelters", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AnimalShelters()
                        {
                            AnimalShelterID = Convert.ToInt32(reader["ID"]),
                            AnimalCapacity = Convert.ToInt32(reader["AnimalCapacity"]),
                            Name = Convert.ToString(reader["Name"]),
                            Address = Convert.ToString(reader["Address"]),
                            PhoneNumber = Convert.ToString(reader["PhoneNumber"]),
                            ImagePath = Convert.ToString(reader["ImagePath"]),
                        });
                    }
                }
            }
            return list;
        }
    }
}