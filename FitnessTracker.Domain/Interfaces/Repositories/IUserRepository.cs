
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<bool> IsUserExistsAsync(
            string email,
            CancellationToken ct = default);

        Task<UserEntity?> GetUserByEmailAsync(
            string email,
            CancellationToken ct = default
            );
    }
}
