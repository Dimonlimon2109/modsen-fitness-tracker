using Microsoft.IdentityModel.Tokens;

namespace FitnessTracker.Application.Exceptions.Auth
{
    public class InvalidTokenException : UnauthorizedException
    {
        public InvalidTokenException(string message) : base(message) { }
    }
}
