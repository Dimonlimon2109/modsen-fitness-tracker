
using AutoMapper;
using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Exceptions.Auth;
using FitnessTracker.Application.Interfaces.Workouts;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces.Repositories;

namespace FitnessTracker.Application.UseCases.Workouts
{
    public class CreateWorkoutUseCase : ICreateWorkoutUseCase
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateWorkoutUseCase(
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
            CreateWorkoutRequest createWorkoutRequest,
            string userEmail,
            CancellationToken ct = default
            )
        {
            var user = await _userRepository.GetUserByEmailAsync(userEmail, ct)
                ?? throw new UserNotFoundException("Пользователь не найден");

            var workout = _mapper.Map<WorkoutEntity>(createWorkoutRequest);

            await _workoutRepository.AddAsync(workout, ct);
            await _workoutRepository.SaveChangesAsync(ct);
        }
    }
}
