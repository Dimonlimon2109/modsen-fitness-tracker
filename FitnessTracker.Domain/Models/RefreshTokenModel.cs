
namespace FitnessTracker.Domain.Models
{
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
