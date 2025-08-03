namespace FitnessTracker.Application.Exceptions.Auth
{
    public class UserNotFoundException(string message) : NotFoundException(message)
    {
    }
}
