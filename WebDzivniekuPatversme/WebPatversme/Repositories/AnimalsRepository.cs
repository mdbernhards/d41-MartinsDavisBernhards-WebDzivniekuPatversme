using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
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
                MySqlCommand cmd = new MySqlCommand("select * from Animals", conn);
                conn.Open();

                using var reader = cmd.ExecuteReader();
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
            return list;
        }

        public void CreateNewAnimal(Animals newAnimal)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("INSERT INTO Animals (ID, Age, Weight, BirthDate, DateAdded, About, Name, Species, Colour, ImagePath, AnimalShelterID) " +
                                                "VALUES (\"" + newAnimal.AnimalID + "\", " + newAnimal.Age + ", " + newAnimal.Weight + ", \"" + newAnimal.BirthDate.Year + "-" + newAnimal.BirthDate.Month
                                                + "-" + newAnimal.BirthDate.Day + "\", \"" + newAnimal.DateAdded.Year + "-" + newAnimal.DateAdded.Month + "-" + newAnimal.DateAdded.Day + "\", \"" +
                                                newAnimal.About + "\", \"" + newAnimal.Name + "\", \"" + newAnimal.Species + "\", \"" + newAnimal.Colour + "\", \"" + newAnimal.ImagePath + "\", \"" +
                                                newAnimal.AnimalShelterId + "\")", conn);

            var reader = cmd.ExecuteReader();
        }

        public void DeleteAnimal(Animals animal)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            conn.Open();

            //string sqlQuerry = "Delete from Animals where ID = \"" + animal.AnimalID + "\";";
            string sqlQuerry = "Delete from Animals where ID = @id ;";


            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            cmd.Prepare();

            cmd.Parameters.AddWithValue("@id", animal.AnimalID);

            var reader = cmd.ExecuteReader();
        }

        public void EditAnimal(Animals newAnimal)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            conn.Open();

            var sqlText = "UPDATE Animals SET Age = @age , Weight = @weight , BirthDate =  @birthDate , DateAdded = @dateAdded , About = @about ," +
                          " Name = @name , Species = @species , Colour = @colour , ImagePath = @imagePath , AnimalShelterID = @animalShelterID WHERE Id = @id";

            MySqlCommand cmd = new MySqlCommand(sqlText, conn);
            cmd.Prepare();

            string birthDateeString = newAnimal.BirthDate.ToString("yyyy-MM-dd");
            string dateAddedString = newAnimal.DateAdded.ToString("yyyy-MM-dd");

            cmd.Parameters.AddWithValue("@age", newAnimal.Age);
            cmd.Parameters.AddWithValue("@weight", newAnimal.Weight);
            cmd.Parameters.AddWithValue("@birthDate", birthDateeString);
            cmd.Parameters.AddWithValue("@dateAdded", dateAddedString);
            cmd.Parameters.AddWithValue("@about", newAnimal.About);
            cmd.Parameters.AddWithValue("@name", newAnimal.Name);
            cmd.Parameters.AddWithValue("@species", newAnimal.Species);
            cmd.Parameters.AddWithValue("@colour", newAnimal.Colour);
            cmd.Parameters.AddWithValue("@imagePath", newAnimal.ImagePath);
            cmd.Parameters.AddWithValue("@animalShelterID", newAnimal.AnimalShelterId);
            cmd.Parameters.AddWithValue("@id", newAnimal.AnimalID);

            var reader = cmd.ExecuteReader();
        }
    }
}