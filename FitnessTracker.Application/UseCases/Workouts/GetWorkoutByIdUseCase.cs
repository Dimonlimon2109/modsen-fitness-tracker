
using AutoMapper;
using FitnessTracker.Application.Contracts.DTOs;
using FitnessTracker.Application.Exceptions.Auth;
using FitnessTracker.Application.Exceptions.Workouts;
using FitnessTracker.Application.Interfaces.Workouts;
using FitnessTracker.Domain.Interfaces.Repositories;

namespace FitnessTracker.Application.UseCases.Workouts
{
    public class GetWorkoutByIdUseCase : IGetWorkoutByIdUseCase
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetWorkoutByIdUseCase(
            IWorkoutRepository workoutRepository,
            IUserRepository userRepository,
            IMapper mapper
            )
        {
            _workoutRepository = workoutRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<WorkoutDTO> ExecuteAsync(
            int id,
            string userEmail,
            CancellationToken ct = default
            )
        {
            var user = await _userRepository.GetUserByEmailAsync(userEmail, ct)
                ?? throw new UserNotFoundException("Пользователь не найден");

            var workout = await _workoutRepository.GetByIdAsync(id, ct)
                ?? throw new WorkoutNotFoundException("Тренировка не найдена");

            if (workout.UserId != user.Id)
            {
                throw new WorkoutForbiddenException("Тренировка другого пользователя недоступна");
            }

            return _mapper.Map<WorkoutDTO>(workout);
        }
    }
}
