
namespace FitnessTracker.Application.Contracts.Responses
{
    public record RefreshTokensResponse
        (
        string AccessToken,
        string RefreshToken
        );
}
