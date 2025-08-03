
using AutoMapper;
using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Exceptions.Auth;
using FitnessTracker.Application.Exceptions.Workouts;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces.Repositories;

namespace FitnessTracker.Application.UseCases.Workouts
{
    public class UpdateWorkoutUseCase
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateWorkoutUseCase(
            IWorkoutRepository workoutRepository,
            IUserRepository userRepository,
            IMapper mapper
            )
        {
            _workoutRepository = workoutRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(
            UpdateWorkoutRequest updateWorkoutRequest,
            string userEmail,
            CancellationToken ct = default)
        {
            var user = await _userRepository.GetUserByEmailAsync(userEmail, ct)
                ?? throw new UserNotFoundException("Пользователь не найден");

            var workout = await _workoutRepository.GetByIdAsync(updateWorkoutRequest.Id, ct)
                ?? throw new WorkoutNotFoundException("Тренировка не найдена");

            if (workout.UserId != user.Id)
            {
                throw new WorkoutForbiddenException("Тренировка другого пользователя недоступна");
            }

            workout.Title = updateWorkoutRequest.Title;
            workout.Type = updateWorkoutRequest.Type;
            workout.Exercises = updateWorkoutRequest.Exercises.Select(ue => _mapper.Map<Exercise>(ue)).ToList();
            workout.Duration = updateWorkoutRequest.Duration;
            workout.CaloriesBurned = updateWorkoutRequest.CaloriesBurned;
            workout.WorkoutDate = updateWorkoutRequest.WorkoutDate;

            _workoutRepository.Update(workout);
            await _workoutRepository.SaveChangesAsync(ct);
        }
    }
}
