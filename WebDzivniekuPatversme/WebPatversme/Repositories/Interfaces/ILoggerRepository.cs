using System.Collections.Generic;
using WebDzivniekuPatversme.Models;

namespace WebDzivniekuPatversme.Repositories.Interfaces
{
    public interface ILoggerRepository
    {
        List<Log> GetLogByID(int id);

        void AddLog (Log log);

        void DeleteItemLogsByID(int id);
    }
}