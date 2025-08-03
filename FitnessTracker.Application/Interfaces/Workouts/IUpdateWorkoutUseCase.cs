using FitnessTracker.Application.Contracts.Requests;

namespace FitnessTracker.Application.Interfaces.Workouts
{
    public interface IUpdateWorkoutUseCase
    {
        Task ExecuteAsync(UpdateWorkoutRequest updateWorkoutRequest, string userEmail, CancellationToken ct = default);
    }
}