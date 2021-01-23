using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebPatversme.Models.Database
{
    public class AnimalsRepository : IAnimalsRepository
    {
        public string ConnectionString { get; set; }

        public AnimalsRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<Animals> GetAllAnimals()
        {
            List<Animals> list = new List<Animals>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Animals", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Animals()
                        {
                            AnimalID = Convert.ToInt32(reader["ID"]),
                            Age = Convert.ToInt32(reader["Age"]),
                            Weight = Convert.ToInt32(reader["Weight"]),
                            BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                            DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                            About = Convert.ToString(reader["About"]),
                            Name = Convert.ToString(reader["Name"]),
                            Species = Convert.ToString(reader["Species"]),
                            Colour = Convert.ToString(reader["Colour"]),
                            ImagePath = Convert.ToString(reader["ImagePath"]),
                            FKAnimalSheltersID = Convert.ToInt32(reader["AnimalShelterID"]),
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