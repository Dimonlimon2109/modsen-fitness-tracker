
namespace FitnessTracker.Application.Contracts.Responses
{
    public record LoginResponse
        (
        string AccessToken,
        string RefreshToken
        );
}
