using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebPatversme.Models.Database
{
    public class ContactsRepository : IContactsRepository
    {
        public string ConnectionString { get; set; }

        public ContactsRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<AnimalShelters> GetAllAnimalShelters()
        {
            List<AnimalShelters> list = new List<AnimalShelters>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Animals", conn);

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

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}