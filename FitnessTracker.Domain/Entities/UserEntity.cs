
namespace FitnessTracker.Domain.Entities
{
    public class UserEntity : EntityBase
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
        public ICollection<WorkoutEntity> Workouts { get; set; } = null!;
    }
}
