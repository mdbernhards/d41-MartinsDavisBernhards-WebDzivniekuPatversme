using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace WebDzivniekuPatversme.Models.Database
{
    public class ShelterDatabaseContext
    {
        public string ConnectionString { get; set; }

        public ShelterDatabaseContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<AnimalShelters> GetAllAnimalShelters()
        {
            List<AnimalShelters> list = new List<AnimalShelters>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from AnimalShelters", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AnimalShelters()
                        {
                            //AnimalShelterID 
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