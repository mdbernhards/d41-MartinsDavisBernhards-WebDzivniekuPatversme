using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Repositories.Interfaces;

namespace WebDzivniekuPatversme.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly WebShelterDbContext _dbcontext;

        public LoggerRepository(
            WebShelterDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public List<Log> GetLogByID(int id)
        {
            List<Log> list = new List<Log>();

            using MySqlConnection conn = _dbcontext.GetConnection();
            var sqlQuerry = "SELECT * FROM log where Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Log()
                {
                    Id = Convert.ToString(reader["Id"]),
                    ItemId = Convert.ToString(reader["ItemId"]),
                    UserId = Convert.ToString(reader["UserId"]),
                    ItemType = Convert.ToInt32(reader["ItemType"]),
                    MessageType = Convert.ToInt32(reader["MessageType"]),
                    TimeStamp = Convert.ToDateTime(reader["TimeStamp"]),
                });
            }

            return list;
        }

        public void AddLog(Log log)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "INSERT INTO log (Id, ItemId, UserId, ItemType, MessageType, TimeStamp) VALUES (@id, @itemId, @userId, @itemType, @messageType, @timeStamp);";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            string timeStampString = log.TimeStamp.ToString("yyyy-MM-dd HH:MM:ss");

            cmd.Parameters.AddWithValue("@id", log.Id);
            cmd.Parameters.AddWithValue("@itemId", log.ItemId);
            cmd.Parameters.AddWithValue("@userId", log.UserId);
            cmd.Parameters.AddWithValue("@itemType", log.ItemType);
            cmd.Parameters.AddWithValue("@messageType", log.MessageType);
            cmd.Parameters.AddWithValue("@timeStamp", timeStampString);

            var reader = cmd.ExecuteReader();
        }

        public void DeleteItemLogsByID(int id)
        {
            using MySqlConnection conn = _dbcontext.GetConnection();
            string sqlQuerry = "DELETE FROM log WHERE Id = @id;";

            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@id", id);

            var reader = cmd.ExecuteReader();
        }
    }
}