

using FitnessTracker.Application.Exceptions.Auth;
using FitnessTracker.Application.Exceptions.Workouts;
using FitnessTracker.Application.Interfaces.Workouts;
using FitnessTracker.Domain.Interfaces.Repositories;

namespace FitnessTracker.Application.UseCases.Workouts
{
    public class DeleteWorkoutUseCase : IDeleteWorkoutUseCase
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IUserRepository _userRepository;

        public DeleteWorkoutUseCase(
            IWorkoutRepository workoutRepository,
            IUserRepository userRepository
            )
        {
            _workoutRepository = workoutRepository;
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(
            int id,
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

            await _workoutRepository.DeleteAsync(id, ct);
            await _workoutRepository.SaveChangesAsync();
        }
    }
}
