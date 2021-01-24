using System;
using WebPatversme.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebDzivniekuPatversme.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly WebShelterDbContext _dbcontext;

        public NewsRepository(
            WebShelterDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public List<News> GetAllNews()
        {
            List<News> list = new List<News>();

            using (MySqlConnection conn = _dbcontext.GetConnection())
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
    }
}