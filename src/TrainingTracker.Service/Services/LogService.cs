using System;
using System.Collections.Generic;
using TrainingTracker.Service.Models;
using TrainingTracker.Service.Repositories;

namespace TrainingTracker.Service.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            if (logRepository == null) throw new ArgumentNullException(nameof(logRepository));
            _logRepository = logRepository;
        }

        public int Add(Log log)
        {
            if (string.IsNullOrEmpty(log.Title))
                log.Title = DateTime.Now.ToString("g");

            if (log.DateAdded == DateTime.MinValue)
                log.DateAdded = DateTime.UtcNow;

            return _logRepository.Add(log);
        }

        public Log GetById(int id)
        {
            return _logRepository.GetById(id);
        }

        public IEnumerable<Log> GetLogsByUserId(int userId)
        {
            return _logRepository.GetLogsByUserId(userId);
        }
    }
}
