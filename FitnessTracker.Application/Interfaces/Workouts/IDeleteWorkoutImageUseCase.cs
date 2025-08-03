namespace FitnessTracker.Application.Interfaces.Workouts
{
    public interface IDeleteWorkoutImageUseCase
    {
        Task ExecuteAsync(int id, string deletingImagePath, string userEmail, CancellationToken ct = default);
    }
}