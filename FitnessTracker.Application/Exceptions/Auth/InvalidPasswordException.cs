namespace FitnessTracker.Application.Exceptions.Auth
{
    public class InvalidPasswordException : BadRequestException
    {
        public InvalidPasswordException(string message) : base(message) { }

    }
}
