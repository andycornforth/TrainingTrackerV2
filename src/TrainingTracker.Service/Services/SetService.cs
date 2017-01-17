using System;
using System.Collections.Generic;
using TrainingTracker.Service.Models;
using TrainingTracker.Service.Repositories;

namespace TrainingTracker.Service.Services
{
    public class SetService : ISetService
    {
        private readonly ISetRepository _setRepository;

        public SetService(ISetRepository setRepository)
        {
            if (setRepository == null) throw new ArgumentNullException(nameof(setRepository));
            _setRepository = setRepository;
        }

        public int Add(Set set)
        {
            if(set.DateAdded == DateTime.MinValue)
                set.DateAdded = DateTime.UtcNow;

            return _setRepository.Add(set);
        }

        public void Update(Set set)
        {
            _setRepository.Update(set);
        }

        public Set GetById(int id)
        {
            return _setRepository.GetById(id);
        }

        public IEnumerable<Set> GetSetsByLogId(int logId)
        {
            return _setRepository.GetSetsByLogId(logId);
        }
    }
}
