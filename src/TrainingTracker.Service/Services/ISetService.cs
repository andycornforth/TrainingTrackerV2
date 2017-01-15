using System.Collections.Generic;
using TrainingTracker.Service.Models;

namespace TrainingTracker.Service.Services
{
    public interface ISetService
    {
        int Add(Set set);
        void Update(Set set);
        Set GetById(int id);
        IEnumerable<Set> GetSetsByLogId(int logId);
    }
}
