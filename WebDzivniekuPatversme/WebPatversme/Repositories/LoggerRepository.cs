using System.Collections.Generic;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Repositories.Interfaces;

namespace WebDzivniekuPatversme.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
        public List<Log> GetLogByID(int id)
        {
            return null;
        }

        public void AddLog(Log log)
        {

        }

        public void DeleteItemsLogsByID(int id)
        {

        }
    }
}