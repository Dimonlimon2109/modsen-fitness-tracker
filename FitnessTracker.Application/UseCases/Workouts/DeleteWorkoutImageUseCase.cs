
using FitnessTracker.Application.Exceptions.Auth;
using FitnessTracker.Application.Exceptions.Workouts;
using FitnessTracker.Application.Interfaces.Workouts;
using FitnessTracker.Domain.Interfaces.Repositories;
using FitnessTracker.Infrastructure.Services;

namespace FitnessTracker.Application.UseCases.Workouts
{
    public class DeleteWorkoutImageUseCase : IDeleteWorkoutImageUseCase
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;

        public DeleteWorkoutImageUseCase(
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
            string deletingImagePath,
            string userEmail,
            CancellationToken ct = default)
        {
            var user = await _userRepository.GetUserByEmailAsync(userEmail, ct)
                            ?? throw new UserNotFoundException("Пользователь не найден");

            var workout = await _workoutRepository.GetByIdAsync(id, ct)
                ?? throw new WorkoutNotFoundException("Тренировка не найдена");

            if (workout.UserId != user.Id)
            {
                throw new WorkoutForbiddenException("Тренировка другого пользователя недоступна");
            }

            if (workout.ProgressPhotos?.FirstOrDefault(deletingImagePath) == null)
            {
                throw new ImageNotFoundException("Изображение не найдено");
            }
            await _imageService.DeleteImageAsync(deletingImagePath);

            workout.ProgressPhotos?.Remove(deletingImagePath);
            _workoutRepository.Update(workout);
            await _workoutRepository.SaveChangesAsync();
        }
    }
}
