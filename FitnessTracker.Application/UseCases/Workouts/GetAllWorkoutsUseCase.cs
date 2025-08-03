
using AutoMapper;
using FitnessTracker.Application.Contracts.DTOs;
using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Contracts.Responses;
using FitnessTracker.Application.Exceptions.Auth;
using FitnessTracker.Domain.Interfaces.Repositories;

namespace FitnessTracker.Application.UseCases.Workouts
{
    public class GetAllWorkoutsUseCase
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllWorkoutsUseCase(IWorkoutRepository workoutRepository, IUserRepository userRepository, IMapper mapper)
        {
            _workoutRepository = workoutRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetAllWorkoutsResponse> ExecuteAsync(
            GetAllWorkoutsRequest getAllWorkoutsRequest,
            string userEmail,
            CancellationToken ct = default
            )
        {
            var user = await _userRepository.GetUserByEmailAsync(userEmail, ct)
                ?? throw new UserNotFoundException("Пользователь не найден");

            var userWorkouts = await _workoutRepository.GetAllWorkoutsWithFiltersAsync(
                user.Id,
                getAllWorkoutsRequest.Title,
                getAllWorkoutsRequest.Type,
                getAllWorkoutsRequest.StartDate,
                getAllWorkoutsRequest.EndDate,
                getAllWorkoutsRequest.MinDuration,
                getAllWorkoutsRequest.MaxDuration,
                getAllWorkoutsRequest.Page,
                getAllWorkoutsRequest.PageSize,
                getAllWorkoutsRequest.SortBy,
                getAllWorkoutsRequest.Order,
                ct);

            var totalPages = await _workoutRepository.GetTotalPagesAsync(
                user.Id,
                getAllWorkoutsRequest.Title,
                getAllWorkoutsRequest.Type,
                getAllWorkoutsRequest.StartDate,
                getAllWorkoutsRequest.EndDate,
                getAllWorkoutsRequest.MinDuration,
                getAllWorkoutsRequest.MaxDuration,
                getAllWorkoutsRequest.PageSize,
                ct);

            var workoutDTO = userWorkouts.Select(uw => _mapper.Map<WorkoutDTO>(uw)).ToList();
            var response = new GetAllWorkoutsResponse(workoutDTO, totalPages);
            return response;
        }
    }
}
