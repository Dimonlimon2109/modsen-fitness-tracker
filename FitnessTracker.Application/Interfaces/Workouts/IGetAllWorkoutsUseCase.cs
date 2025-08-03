using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Contracts.Responses;

namespace FitnessTracker.Application.Interfaces.Workouts
{
    public interface IGetAllWorkoutsUseCase
    {
        Task<GetAllWorkoutsResponse> ExecuteAsync(GetAllWorkoutsRequest getAllWorkoutsRequest, string userEmail, CancellationToken ct = default);
    }
}