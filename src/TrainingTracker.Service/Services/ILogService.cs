using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingTracker.Service.Models;

namespace TrainingTracker.Service.Services
{
    public interface ILogService
    {
        int Add(Log log);
        Log GetById(int id);
        IEnumerable<Log> GetLogsByUserId(int userId);
    }
}
