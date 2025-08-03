
using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Application.Contracts.DTOs
{
    public record WorkoutDTO
        (
        int Id,
        string Title,
        WorkoutType Type,
        List<ExerciseDTO> Exercises,
        TimeSpan Duration,
        int CaloriesBurned,
        List<string>? ProgressPhotos,
        DateTime WorkoutDate,
        DateTime CreatedAt
        );
}
