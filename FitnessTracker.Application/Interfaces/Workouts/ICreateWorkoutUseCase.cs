using FitnessTracker.Application.Contracts.Requests;

namespace FitnessTracker.Application.Interfaces.Workouts
{
    public interface ICreateWorkoutUseCase
    {
        Task ExecuteAsync(CreateWorkoutRequest createWorkoutRequest, string userEmail, CancellationToken ct = default);
    }
}