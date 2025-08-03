
using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Application.Contracts.DTOs
{
    public class WorkoutDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string WorkoutType { get; set; }
        public List<ExerciseDTO> Exercises { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public List<string>? ProgressPhotos { get; set; }
        public DateTime WorkoutDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
