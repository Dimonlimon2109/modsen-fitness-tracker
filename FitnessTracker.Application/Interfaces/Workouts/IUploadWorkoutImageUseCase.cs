using FitnessTracker.Domain.Interfaces.Adapters;

namespace FitnessTracker.Application.Interfaces.Workouts
{
    public interface IUploadWorkoutImageUseCase
    {
        Task ExecuteAsync(int id, IImageFile image, string userEmail, CancellationToken ct = default);
    }
}