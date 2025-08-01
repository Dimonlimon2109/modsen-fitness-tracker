namespace FitnessTracker.Application.Exceptions.Auth
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string message) : base(message) { }
    }
}
