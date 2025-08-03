
using FitnessTracker.Application.Contracts.DTOs;
using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Application.Contracts.Requests
{
    public record CreateWorkoutRequest
        (
        string Title,
        WorkoutType Type,
        IEnumerable<ExerciseDTO> Exercises,
        TimeSpan Duration,
        int CaloriesBurned,
        DateTime WorkoutDate
        );
}
