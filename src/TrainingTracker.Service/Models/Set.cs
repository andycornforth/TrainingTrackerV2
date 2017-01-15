using System;

namespace TrainingTracker.Service.Models
{
    /// <summary>
    /// Represents one set of an exercise.
    /// </summary>
    public class Set
    {
        /// <summary>
        /// The id of the set.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The log id of which the set belongs to.
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// The exercise id.
        /// </summary>
        public int ExerciseId { get; set; }

        /// <summary>
        /// The weight of the equipment used in the set.
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// The number of reps accomplished in the set.
        /// </summary>
        public int Reps { get; set; }

        /// <summary>
        /// The date and time the set was added.
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// The position of the set in the log (workout).
        /// </summary>
        public int Position { get; set; }
    }
}
