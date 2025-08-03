
using FitnessTracker.Application.Contracts.DTOs;

namespace FitnessTracker.Application.Contracts.Responses
{
    public record GetAllWorkoutsResponse
        (
        List<WorkoutDTO>? Workouts,
        int TotalPages
        );
}
