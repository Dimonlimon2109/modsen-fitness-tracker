namespace FitnessTracker.Application.Exceptions.Auth
{
    
    public class UserAlreadyExistsException(string message) : ConflictException(message)
    {
    }
}
