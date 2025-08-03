namespace FitnessTracker.Application.Interfaces.Workouts
{
    public interface IDeleteWorkoutUseCase
    {
        Task ExecuteAsync(int id, string userEmail, CancellationToken ct = default);
    }
}