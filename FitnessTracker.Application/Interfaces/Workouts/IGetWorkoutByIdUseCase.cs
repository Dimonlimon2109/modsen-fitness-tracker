using FitnessTracker.Application.Contracts.DTOs;

namespace FitnessTracker.Application.Interfaces.Workouts
{
    public interface IGetWorkoutByIdUseCase
    {
        Task<WorkoutDTO> ExecuteAsync(int id, string userEmail, CancellationToken ct = default);
    }
}