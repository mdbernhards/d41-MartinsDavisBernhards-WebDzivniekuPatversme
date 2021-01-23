using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebPatversme.Models.Database
{
    public class NewsRepository : INewsRepository
    {
        public string ConnectionString { get; set; }

        public NewsRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<News> GetAllNews()
        {
            List<News> list = new List<News>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from News", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new News()
                        {
                            NewsID = Convert.ToInt32(reader["ID"]),
                            DateCreated = Convert.ToDateTime(reader["DateCreated"]),
                            Text = Convert.ToString(reader["Text"]),
                            ImagePath = Convert.ToString(reader["ImagePath"]),
                            FKUsersID = Convert.ToInt32(reader["UsersID"]),
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