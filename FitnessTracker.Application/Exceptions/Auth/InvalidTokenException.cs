using Microsoft.IdentityModel.Tokens;

namespace FitnessTracker.Application.Exceptions.Auth
{
    public class InvalidTokenException(string message) : UnauthorizedException(message)
    {
    }
}
