using System.Collections.Generic;
using TrainingTracker.Service.Models;

namespace TrainingTracker.Service.Repositories
{
    public interface ISetRepository
    {
        int Add(Set set);
        void Update(Set set);
        Set GetById(int id);
        IEnumerable<Set> GetSetsByLogId(int logId);
    }
}
