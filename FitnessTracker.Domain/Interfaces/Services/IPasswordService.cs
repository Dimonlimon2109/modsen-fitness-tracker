
namespace FitnessTracker.Domain.Interfaces.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool ValidatePassword(string password, string hashedPassword);
    }
}
