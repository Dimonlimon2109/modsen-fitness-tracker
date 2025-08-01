
using FitnessTracker.Domain.Interfaces.Services;

namespace FitnessTracker.Infrastructure.Services
{
    public class BCryptPasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool ValidatePassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
