﻿using System;
using System.Collections.Generic;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Repositories.Interfaces;
using MySql.Data.MySqlClient;

namespace WebDzivniekuPatversme.Repositories
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

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM Animals;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Animals animal = new Animals()
                {
                    AnimalID = Convert.ToString(reader["ID"]),
                    Weight = Convert.ToInt32(reader["Weight"]),
                    BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                    DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                    About = Convert.ToString(reader["About"]),
                    Name = Convert.ToString(reader["Name"]),
                    Species = Convert.ToString(reader["Species"]),
                    Colour = Convert.ToString(reader["Colour"]),
                    ImagePath = Convert.ToString(reader["ImagePath"]),
                    AnimalShelterId = Convert.ToString(reader["AnimalShelterID"]),
                };

                animal.Age = CalculateAge(animal.BirthDate);

                list.Add(animal);
            }
            return list;
        }

        public void CreateNewAnimal(Animals newAnimal)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO Animals (ID, Weight, BirthDate, DateAdded, About, Name, Species, Colour, ImagePath, AnimalShelterID) " +
                                                "VALUES (@id, @weight, @birthDate, @dateAdded, @about, @name, @species, @colour, @imagePath, @animalShelterId);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string birthDateeString = newAnimal.BirthDate.ToString("yyyy-MM-dd");
            string dateAddedString = newAnimal.DateAdded.ToString("yyyy-MM-dd");

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

        public void DeleteAnimal(Animals animal)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "DELETE FROM Animals WHERE ID = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", animal.AnimalID);

            var reader = cmd.ExecuteReader();
        }

        public void EditAnimal(Animals animal)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "UPDATE Animals SET Weight = @weight , BirthDate =  @birthDate , About = @about ," +
                          " Name = @name , Species = @species , Colour = @colour , ImagePath = @imagePath , AnimalShelterID = @animalShelterID WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string birthDateeString = animal.BirthDate.ToString("yyyy-MM-dd");

            cmd.Parameters.AddWithValue("@weight", animal.Weight);
            cmd.Parameters.AddWithValue("@birthDate", birthDateeString);
            cmd.Parameters.AddWithValue("@about", animal.About);
            cmd.Parameters.AddWithValue("@name", animal.Name);
            cmd.Parameters.AddWithValue("@species", animal.Species);
            cmd.Parameters.AddWithValue("@colour", animal.Colour);
            cmd.Parameters.AddWithValue("@imagePath", animal.ImagePath);
            cmd.Parameters.AddWithValue("@animalShelterID", animal.AnimalShelterId);
            cmd.Parameters.AddWithValue("@id", animal.AnimalID);

            var reader = cmd.ExecuteReader();
        }

        private int CalculateAge(DateTime birthDate)
        {
            var age = DateTime.Now.Year - birthDate.Year;

            if (birthDate > DateTime.Now.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}