
namespace FitnessTracker.Application.Contracts.Requests
{
    public record RefreshTokensRequest
        (
        string AccessToken,
        string RefreshToken
        );
}
