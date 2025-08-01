using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Contracts.Responses;

namespace FitnessTracker.Application.Interfaces.Auth
{
    public interface IRefreshTokensUseCase
    {
        Task<RefreshTokensResponse> ExecuteAsync(RefreshTokensRequest refreshTokensRequest, CancellationToken ct = default);
    }
}