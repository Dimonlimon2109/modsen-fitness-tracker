using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Contracts.Responses;

namespace FitnessTracker.Application.Interfaces.Auth
{
    public interface ILoginUseCase
    {
        Task<LoginResponse> ExecuteAsync(LoginRequest loginRequest, CancellationToken ct = default);
    }
}