using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Hosting;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Repositories.Interfaces;
using WebDzivniekuPatversme.Models.ViewModels.Animal;

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

        public List<Animal> GetAllAnimals()
        {
            List<Animal> list = new List<Animal>();

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM Animals;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Animal animal = new Animal()
                {
                    Id = Convert.ToString(reader["Id"]),
                    Weight = Convert.ToDouble(reader["Weight"]),
                    BirthDate = Convert.ToDateTime(reader["BirthDate"].Equals(DBNull.Value) ? null : reader["BirthDate"]),
                    BirthDateRangeTo = Convert.ToDateTime(reader["BirthDateRangeTo"].Equals(DBNull.Value) ? null : reader["BirthDate"]),
                    DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                    About = Convert.ToString(reader["About"]),
                    Name = Convert.ToString(reader["Name"]),
                    Gender = Convert.ToString(reader["Gender"]),
                    Species = Convert.ToString(reader["Species"]),
                    SpeciesType = Convert.ToString(reader["Type"]),
                    Colour = Convert.ToString(reader["Colour"]),
                    SecondaryColour = Convert.ToString(reader["SecondaryColour"]),
                    ImagePath = Convert.ToString(reader["ImagePath"]),
                    ShelterId = Convert.ToString(reader["ShelterId"]),
                };

                animal.Age = CalculateAge(animal.BirthDate);
                list.Add(animal);
            }

            return list;
        }

        public void CreateNewAnimal(Animal animal)
        {
            animal.ImagePath = SaveImage(animal);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO Animals (Id, Weight, BirthDate, BirthDateRangeTo, DateAdded, About, Name, Gender, Species, Type, Colour, SecondaryColour, ImagePath, ShelterId) " +
                "VALUES (@id, @weight, @birthDate, @birthDateRangeTo, @dateAdded, @about, @name, @gender, @species, @type, @colour, @secondaryColour, @imagePath, @shelterId);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string birthDateString = animal.BirthDate.HasValue ? animal.BirthDate.Value.ToString("yyyy-MM-dd") : "1000-10-10";
            string birthDateRangeToString = animal.BirthDateRangeTo.HasValue ? animal.BirthDateRangeTo.Value.ToString("yyyy-MM-dd") : "1000-10-10";
            string dateAddedString = animal.DateAdded.ToString("yyyy-MM-dd HH:MM:ss");

            cmd.Parameters.AddWithValue("@weight", animal.Weight);
            cmd.Parameters.AddWithValue("@birthDate", birthDateString);
            cmd.Parameters.AddWithValue("@birthDateRangeTo", birthDateRangeToString);
            cmd.Parameters.AddWithValue("@dateAdded", dateAddedString);
            cmd.Parameters.AddWithValue("@about", animal.About);
            cmd.Parameters.AddWithValue("@name", animal.Name);
            cmd.Parameters.AddWithValue("@gender", animal.Gender);
            cmd.Parameters.AddWithValue("@species", animal.Species);
            cmd.Parameters.AddWithValue("@type", animal.SpeciesType);
            cmd.Parameters.AddWithValue("@colour", animal.Colour);
            cmd.Parameters.AddWithValue("@secondaryColour", animal.SecondaryColour);
            cmd.Parameters.AddWithValue("@imagePath", animal.ImagePath);
            cmd.Parameters.AddWithValue("@shelterId", animal.ShelterId);
            cmd.Parameters.AddWithValue("@id", animal.Id);

            var reader = cmd.ExecuteReader();
        }

        public void DeleteAnimal(Animal animal)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "DELETE FROM Animals WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", animal.Id);

            var reader = cmd.ExecuteReader();
        }

        public void EditAnimal(Animal animal)
        {
            animal.ImagePath = SaveImage(animal);

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "UPDATE Animals SET Weight = @weight, BirthDate =  @birthDate, BirthDateRangeTo = @birthDateRangeTo, About = @about," +
                " Name = @name, Gender = @gender, Species = @species, Type = @type, Colour = @colour, SecondaryColour = @secondaryColour, ImagePath = @imagePath, ShelterId = @shelterId WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string birthDateString = animal.BirthDate.HasValue ? animal.BirthDate.Value.ToString("yyyy-MM-dd") : "1000-10-10";
            string birthDateRangeToString = animal.BirthDateRangeTo.HasValue ? animal.BirthDateRangeTo.Value.ToString("yyyy-MM-dd") : "1000-10-10";

            cmd.Parameters.AddWithValue("@weight", animal.Weight);
            cmd.Parameters.AddWithValue("@birthDate", birthDateString);
            cmd.Parameters.AddWithValue("@birthDateRangeTo", birthDateRangeToString);
            cmd.Parameters.AddWithValue("@about", animal.About);
            cmd.Parameters.AddWithValue("@name", animal.Name);
            cmd.Parameters.AddWithValue("@gender", animal.Gender);
            cmd.Parameters.AddWithValue("@species", animal.Species);
            cmd.Parameters.AddWithValue("@type", animal.SpeciesType);
            cmd.Parameters.AddWithValue("@colour", animal.Colour);
            cmd.Parameters.AddWithValue("@secondaryColour", animal.SecondaryColour);
            cmd.Parameters.AddWithValue("@imagePath", animal.ImagePath);
            cmd.Parameters.AddWithValue("@shelterId", animal.ShelterId);
            cmd.Parameters.AddWithValue("@id", animal.Id);

            var reader = cmd.ExecuteReader();
        }

        public void CreateNewColour(AnimalColourViewModel colour)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO Colours (Id, Name) " +
                "VALUES (@id, @name);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", colour.Id);
            cmd.Parameters.AddWithValue("@name", colour.Name);

            var reader = cmd.ExecuteReader();
        }

        public List<AnimalColourViewModel> GetAllColours()
        {
            List<AnimalColourViewModel> colourList = new List<AnimalColourViewModel>();

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM Colours;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var colour = new AnimalColourViewModel
                {
                    Id = Convert.ToString(reader["Id"]),
                    Name = Convert.ToString(reader["Name"]),
                };

                colourList.Add(colour);
            }

            RemoveNoColourOption(colourList);

            return colourList;
        }

        public void DeleteColour(AnimalColourViewModel colour)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "DELETE FROM Colours WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", colour.Id);

            var reader = cmd.ExecuteReader();
        }

        public void CreateNewSpecies(AnimalSpeciesViewModel species)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO Species (Id, Name) " +
                "VALUES (@id, @name);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", species.Id);
            cmd.Parameters.AddWithValue("@name", species.Name);

            var reader = cmd.ExecuteReader();
        }

        public List<AnimalSpeciesViewModel> GetAllSpecies()
        {
            List<AnimalSpeciesViewModel> speciesList = new List<AnimalSpeciesViewModel>();

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM Species;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var species = new AnimalSpeciesViewModel
                {
                    Id = Convert.ToString(reader["Id"]),
                    Name = Convert.ToString(reader["Name"]),
                };

                speciesList.Add(species);
            }

            return speciesList;
        }

        public void DeleteSpecies(AnimalSpeciesViewModel species)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "DELETE FROM Species WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", species.Id);

            var reader = cmd.ExecuteReader();
        }

        public void CreateNewSpeciesType(AnimalSpeciesTypeViewModel speciesType)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "INSERT INTO SpeciesTypes (Id, Name, SpeciesId) " +
                "VALUES (@id, @name, @speciesId);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", speciesType.Id);
            cmd.Parameters.AddWithValue("@name", speciesType.Name);
            cmd.Parameters.AddWithValue("@speciesId", speciesType.SpeciesId);

            var reader = cmd.ExecuteReader();
        }

        public List<AnimalSpeciesTypeViewModel> GetAllSpeciesTypes()
        {
            List<AnimalSpeciesTypeViewModel> speciesList = new List<AnimalSpeciesTypeViewModel>();

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM SpeciesTypes;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var species = new AnimalSpeciesTypeViewModel
                {
                    Id = Convert.ToString(reader["Id"]),
                    Name = Convert.ToString(reader["Name"]),
                    SpeciesId = Convert.ToString(reader["SpeciesId"]),
                };

                speciesList.Add(species);
            }

            return speciesList;
        }

        public void DeleteSpeciesType(AnimalSpeciesTypeViewModel speciesType)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "DELETE FROM SpeciesTypes WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", speciesType.Id);

            var reader = cmd.ExecuteReader();
        }

        private static void RemoveNoColourOption(List<AnimalColourViewModel> colourList)
        {
            colourList
                .Remove(colourList
                .Where(x => x.Name == "Nav")
                .FirstOrDefault());
        }

        private static string CalculateAge(DateTime? birthDate)
        {
            int Years = DateTime.Now.Year - birthDate.Value.Year;
            int Months;
            int Days;
            string Age;

            if (DateTime.Now.Year == birthDate.Value.Year)
            {
                Months = DateTime.Now.Month - birthDate.Value.Month;
            }
            else
            {
                Months = (12 - birthDate.Value.Month) + DateTime.Now.Month;
            }

            if (DateTime.Now.Month == birthDate.Value.Month)
            {
                Days = DateTime.Now.Day - birthDate.Value.Day;
            }
            else
            {
                Days = (DateTime.DaysInMonth(birthDate.Value.Year, birthDate.Value.Month) - birthDate.Value.Day) + DateTime.Now.Day;
            }

            if (birthDate > DateTime.Now.AddYears(-Years))
            {
                Years--;
            }

            if (Years == 0)
            {
                if (Days < 31 && Months < 2)
                {
                    if (Days == 1)
                    {
                        Age = "1 diena";
                    }
                    else if (Days == 0)
                    {
                        Age = "<1 diena";
                    }
                    else
                    {
                        Age = Days.ToString() + " dienas";
                    }
                }
                else if (Months == 1)
                {
                    Age = "1 mēnesis";
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
                else if (Years == 1)
                {
                    Age = "1 gads";
                }
                else
                {
                    Age = Years.ToString() + " gadi";
                }
            }

            return Age;
        }

        private string SaveImage(Animal animal)
        {
            if (animal.Image != null && animal.Image.Length > 0)
            {
                var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\images\\animals");
                var fileName = Path
                    .GetFileName(animal.Name + animal.Id + Path
                    .GetExtension(animal.Image.FileName));

                File.Delete(Path.Combine(uploads, fileName));

                var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
                animal.Image.CopyTo(fileStream);
                fileStream.Close();

                return fileName;
            }

            return GetAllAnimals()
                .Where(x => x.Id == animal.Id)
                .FirstOrDefault()?.ImagePath;
        }
    }
}