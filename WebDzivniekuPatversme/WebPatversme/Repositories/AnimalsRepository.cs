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

        public void CreateNewColour(AnimalColour colour)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO Colours (ID, Name) " +
                "VALUES (@id, @name);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", colour.Id);
            cmd.Parameters.AddWithValue("@name", colour.Name);

            var reader = cmd.ExecuteReader();
        }

        public List<AnimalColour> GetAllColours()
        {
            List<AnimalColour> colourList = new List<AnimalColour>();

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM Colours;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var colour = new AnimalColour
                {
                    Id = Convert.ToString(reader["Id"]),
                    Name = Convert.ToString(reader["Name"]),
                };

                colourList.Add(colour);
            }

            return colourList;
        }

        public void DeleteColour(AnimalColour colour)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "DELETE FROM Colours WHERE ID = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", colour.Id);

            var reader = cmd.ExecuteReader();
        }

        public void CreateNewSpecies(AnimalSpecies species)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO Species (ID, Name) " +
                "VALUES (@id, @name);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", species.Id);
            cmd.Parameters.AddWithValue("@name", species.Name);

            var reader = cmd.ExecuteReader();
        }

        public List<AnimalSpecies> GetAllSpecies()
        {
            List<AnimalSpecies> speciesList = new List<AnimalSpecies>();

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM Species;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var species = new AnimalSpecies
                {
                    Id = Convert.ToString(reader["Id"]),
                    Name = Convert.ToString(reader["Name"]),
                };

                speciesList.Add(species);
            }

            return speciesList;
        }

        public void DeleteSpecies(AnimalSpecies species)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "DELETE FROM Species WHERE ID = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", species.Id);

            var reader = cmd.ExecuteReader();
        }

        public void CreateNewSpeciesType(AnimalSpeciesType speciesType)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO SpeciesTypes (ID, Name, SpeciesId) " +
                "VALUES (@id, @name, @speciesId);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", speciesType.Id);
            cmd.Parameters.AddWithValue("@name", speciesType.Name);
            cmd.Parameters.AddWithValue("@speciesId", speciesType.SpeciesId);

            var reader = cmd.ExecuteReader();
        }

        public List<AnimalSpeciesType> GetAllSpeciesTypes()
        {
            List<AnimalSpeciesType> speciesList = new List<AnimalSpeciesType>();

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM SpeciesTypes;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var species = new AnimalSpeciesType
                {
                    Id = Convert.ToString(reader["Id"]),
                    Name = Convert.ToString(reader["Name"]),
                    SpeciesId = Convert.ToString(reader["SpeciesId"]),
                };

                speciesList.Add(species);
            }

            return speciesList;
        }

        public void DeleteSpeciesType(AnimalSpeciesType speciesType)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "DELETE FROM SpeciesTypes WHERE ID = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", speciesType.Id);

            var reader = cmd.ExecuteReader();
        }

        private static string CalculateAge(DateTime? birthDate)
        {
            string Age;

            int Years = DateTime.Now.Year - birthDate.Value.Year;
            int Months = DateTime.Now.Month - birthDate.Value.Month;
            int Days = DateTime.Now.Day - birthDate.Value.Day;

            if (birthDate > DateTime.Now.AddYears(-Years))
            {
                Years--;
            }

            if (Years == 0)
            {
                if (Months == 0)
                {
                    Age = Days.ToString() + " dienas";
                }
                else
                {
                    Age = Months.ToString() + " mēneši";
                }
            }
            else
            {
                if (Months > 5 && Months < 13)
                {
                    Age = Years.ToString() + ".5 gadi";
                }
                else
                {
                    Age = Years.ToString() + " gadi";
                }

            }

            return Age;
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