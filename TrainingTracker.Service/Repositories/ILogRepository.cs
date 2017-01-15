using System.Collections.Generic;
using TrainingTracker.Service.Models;

namespace TrainingTracker.Service.Repositories
{
    public interface ILogRepository
    {
        int Add(Log log);
        Log GetById(int id);
        IEnumerable<Log> GetLogsByUserId(int userId);
    }
}
