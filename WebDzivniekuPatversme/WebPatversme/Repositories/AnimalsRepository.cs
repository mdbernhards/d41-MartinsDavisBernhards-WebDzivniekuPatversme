using System;
using WebPatversme.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebDzivniekuPatversme.Repository
{
    public class AnimalsRepository : IAnimalsRepository
    {
        private readonly WebShelterDbContext _dbcontext;

        public AnimalsRepository(
            WebShelterDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public List<Animals> GetAllAnimals()
        {
            List<Animals> list = new List<Animals>();

            using (MySqlConnection conn = _dbcontext.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Animals", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Animals()
                        {
                            AnimalID = Convert.ToString(reader["ID"]),
                            Age = Convert.ToInt32(reader["Age"]),
                            Weight = Convert.ToInt32(reader["Weight"]),
                            BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                            DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                            About = Convert.ToString(reader["About"]),
                            Name = Convert.ToString(reader["Name"]),
                            Species = Convert.ToString(reader["Species"]),
                            Colour = Convert.ToString(reader["Colour"]),
                            ImagePath = Convert.ToString(reader["ImagePath"]),
                            AnimalShelterId = Convert.ToString(reader["AnimalShelterID"]),
                        });
                    }
                }
            }
            return list;
        }

        public void CreateNewAnimal(Animals newAnimal)
        {
            using (MySqlConnection conn = _dbcontext.GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO Animals (ID, Age, Weight, BirthDate, DateAdded, About, Name, Species, Colour, ImagePath, AnimalShelterID) "   +
                                                    "VALUES (\"" + newAnimal.AnimalID + "\", " + newAnimal.Age + ", " + newAnimal.Weight + ", \"" + newAnimal.BirthDate.Year + "-" + newAnimal.BirthDate.Month
                                                    + "-" + newAnimal.BirthDate.Day + "\", \"" + newAnimal.DateAdded.Year + "-" + newAnimal.DateAdded.Month  + "-"  + newAnimal.DateAdded.Day + "\", \"" +
                                                    newAnimal.About + "\", \""  + newAnimal.Name + "\", \"" + newAnimal.Species + "\", \""  + newAnimal.Colour + "\", \"" + newAnimal.ImagePath + "\", \"" +
                                                    newAnimal.AnimalShelterId + "\")", conn);

                var reader = cmd.ExecuteReader();
            }
        }
    }
}