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
    public class AnimalsRepository : IAnimalsRepository
    {
        private readonly WebShelterDbContext _dbcontext;
        private readonly IWebHostEnvironment _appEnvironment;

        public AnimalsRepository(
            WebShelterDbContext dbContext, 
            IWebHostEnvironment appEnvironment)
        {
            _dbcontext = dbContext;
            _appEnvironment = appEnvironment;
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
                var one = reader["BirthDate"].GetType();
                var two = reader["ID"];
                var three = reader["BirthDateRangeTo"].GetType();

                if(three.GetType().Equals(DBNull.Value))
                {
                    //pog
                }

                Animals animal = new Animals()
                {
                    AnimalID = Convert.ToString(reader["ID"]),
                    Weight = Convert.ToDouble(reader["Weight"]),
                    BirthDate = Convert.ToDateTime(reader["BirthDate"].Equals(DBNull.Value) ? null : reader["BirthDate"]),
                    BirthDateRangeTo = Convert.ToDateTime(reader["BirthDateRangeTo"].Equals(DBNull.Value) ? null : reader["BirthDate"]),
                    DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                    About = Convert.ToString(reader["About"]),
                    Name = Convert.ToString(reader["Name"]),
                    Species = Convert.ToString(reader["Species"]),
                    SpeciesType = Convert.ToString(reader["Type"]),
                    Colour = Convert.ToString(reader["Colour"]),
                    SecondaryColour = Convert.ToString(reader["SecondaryColour"]),
                    ImagePath = Convert.ToString(reader["ImagePath"]),
                    AnimalShelterId = Convert.ToString(reader["AnimalShelterID"]),
                };

                animal.Age = CalculateAge(animal.BirthDate);

                list.Add(animal);
            }
            return list;
        }

        public void CreateNewAnimal(Animals animal)
        {
            animal.ImagePath = SaveImage(animal);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO Animals (ID, Weight, BirthDate, BirthDateRangeTo, DateAdded, About, Name, Species, Type, Colour, SecondaryColour, ImagePath, AnimalShelterID) " +
                                                "VALUES (@id, @weight, @birthDate, @birthDateRangeTo, @dateAdded, @about, @name, @species, @type, @colour, @secondaryColour, @imagePath, @animalShelterId);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string birthDateString = animal.BirthDate.Value.ToString("yyyy-MM-dd");
            string birthDateRangeToString = animal.BirthDateRangeTo.Value.ToString("yyyy-MM-dd");
            string dateAddedString = animal.DateAdded.ToString("yyyy-MM-dd HH:MM:ss");

            cmd.Parameters.AddWithValue("@weight", animal.Weight);
            cmd.Parameters.AddWithValue("@birthDate", birthDateString);
            cmd.Parameters.AddWithValue("@birthDateRangeTo", birthDateRangeToString);
            cmd.Parameters.AddWithValue("@dateAdded", dateAddedString);
            cmd.Parameters.AddWithValue("@about", animal.About);
            cmd.Parameters.AddWithValue("@name", animal.Name);
            cmd.Parameters.AddWithValue("@species", animal.Species);
            cmd.Parameters.AddWithValue("@type", animal.SpeciesType);
            cmd.Parameters.AddWithValue("@colour", animal.Colour);
            cmd.Parameters.AddWithValue("@secondaryColour", animal.SecondaryColour);
            cmd.Parameters.AddWithValue("@imagePath", animal.ImagePath);
            cmd.Parameters.AddWithValue("@animalShelterID", animal.AnimalShelterId);
            cmd.Parameters.AddWithValue("@id", animal.AnimalID);

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
            animal.ImagePath = SaveImage(animal);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "UPDATE Animals SET Weight = @weight, BirthDate =  @birthDate, BirthDateRangeTo = @birthDateRangeTo, About = @about," +
                          " Name = @name, Species = @species, Type = @type, Colour = @colour, SecondaryColour = @secondaryColour, ImagePath = @imagePath, AnimalShelterID = @animalShelterID WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string birthDateString = animal.BirthDate.Value.ToString("yyyy-MM-dd");
            string birthDateRangeToString = animal.BirthDateRangeTo.Value.ToString("yyyy-MM-dd");

            cmd.Parameters.AddWithValue("@weight", animal.Weight);
            cmd.Parameters.AddWithValue("@birthDate", birthDateString);
            cmd.Parameters.AddWithValue("@birthDateRangeTo", birthDateRangeToString);
            cmd.Parameters.AddWithValue("@about", animal.About);
            cmd.Parameters.AddWithValue("@name", animal.Name);
            cmd.Parameters.AddWithValue("@species", animal.Species);
            cmd.Parameters.AddWithValue("@type", animal.SpeciesType);
            cmd.Parameters.AddWithValue("@colour", animal.Colour);
            cmd.Parameters.AddWithValue("@secondaryColour", animal.SecondaryColour);
            cmd.Parameters.AddWithValue("@imagePath", animal.ImagePath);
            cmd.Parameters.AddWithValue("@animalShelterID", animal.AnimalShelterId);
            cmd.Parameters.AddWithValue("@id", animal.AnimalID);

            var reader = cmd.ExecuteReader();
        }

        private static string CalculateAge(DateTime? birthDate)
        {
            var age = DateTime.Now.Year - birthDate.Value.Year;

            if (birthDate > DateTime.Now.AddYears(-age))
            {
                age--;
            }

            return age.ToString();
        }

        private string SaveImage(Animals animal)
        {
            var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\images\\animals");

            if (animal.Image != null && animal.Image.Length > 0)
            {
                var fileName = Path.GetFileName(animal.Name + animal.AnimalID + Path.GetExtension(animal.Image.FileName));

                File.Delete(Path.Combine(uploads, fileName));

                var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
                animal.Image.CopyTo(fileStream);
                fileStream.Close();

                return fileName;
            }

            return string.Empty;
        }
    }
}