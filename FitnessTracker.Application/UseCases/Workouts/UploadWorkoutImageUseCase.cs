
using FitnessTracker.Application.Exceptions.Auth;
using FitnessTracker.Application.Exceptions.Workouts;
using FitnessTracker.Application.Interfaces.Workouts;
using FitnessTracker.Domain.Interfaces.Adapters;
using FitnessTracker.Domain.Interfaces.Repositories;
using FitnessTracker.Infrastructure.Services;

namespace FitnessTracker.Application.UseCases.Workouts
{
    public class UploadWorkoutImageUseCase : IUploadWorkoutImageUseCase
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;

        public UploadWorkoutImageUseCase(
            IWorkoutRepository workoutRepository,
            IUserRepository userRepository,
            IImageService imageService)
        {
            _workoutRepository = workoutRepository;
            _userRepository = userRepository;
            _imageService = imageService;
        }

        public async Task ExecuteAsync(
            int id,
            IImageFile image,
            string userEmail,
            CancellationToken ct = default)
        {
            var user = await _userRepository.GetUserByEmailAsync(userEmail, ct)
                            ?? throw new UserNotFoundException("User not found");

            var workout = await _workoutRepository.GetByIdAsync(id, ct)
                ?? throw new WorkoutNotFoundException("Workout not found");

            if (workout.UserId != user.Id)
            {
                throw new WorkoutForbiddenException("Another user's workout is unavailable");
            }

            var imagePath = await _imageService.UploadImageAsync(image, ct);

            if (workout.ProgressPhotos == null)
                workout.ProgressPhotos = new();
            workout.ProgressPhotos.Add(imagePath);
            _workoutRepository.Update(workout);
            await _workoutRepository.SaveChangesAsync();
        }
    }
}
