using System;

namespace TrainingTracker.Service.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
