
using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Domain.Entities
{
    public class WorkoutEntity : EntityBase
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public WorkoutType Type { get; set; }
        public List<Exercise> Exercises { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public List<string> ProgressPhotos { get; set; }
        public DateTime WorkoutDate { get; set; }
    }
}
