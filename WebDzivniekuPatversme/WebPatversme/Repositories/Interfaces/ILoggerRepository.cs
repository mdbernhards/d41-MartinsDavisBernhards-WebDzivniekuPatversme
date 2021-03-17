using System.Collections.Generic;
using WebDzivniekuPatversme.Models;

namespace WebDzivniekuPatversme.Repositories.Interfaces
{
    public interface ILoggerRepository
    {
        List<Log> GetLogById(int id);

        void AddLog (Log log);

        void DeleteItemLogsById(int id);
    }
}