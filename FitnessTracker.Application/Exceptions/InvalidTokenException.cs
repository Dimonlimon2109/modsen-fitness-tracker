
using Microsoft.IdentityModel.Tokens;

namespace FitnessTracker.Application.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string message) : base(message) { }
    }
}
