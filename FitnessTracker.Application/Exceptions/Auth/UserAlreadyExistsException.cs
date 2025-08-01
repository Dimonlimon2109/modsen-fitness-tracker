namespace FitnessTracker.Application.Exceptions.Auth
{
    
    public class UserAlreadyExistsException : ConflictException
    {
        public UserAlreadyExistsException(string message) : base(message) { }

    }
}
