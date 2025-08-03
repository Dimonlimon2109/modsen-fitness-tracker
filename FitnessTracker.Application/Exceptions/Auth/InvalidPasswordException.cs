namespace FitnessTracker.Application.Exceptions.Auth
{
    public class InvalidPasswordException(string message) : BadRequestException(message)
    {
    }
}
