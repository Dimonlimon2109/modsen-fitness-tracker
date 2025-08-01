
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repositories
{
    public class UserRepository(FitnessDbContext context) : Repository<UserEntity>(context), IUserRepository
    {
        public async Task<bool> IsUserExistsAsync(
            string email,
            CancellationToken ct = default
            )
        {
            return await _dbSet.AnyAsync(u => u.Email == email, ct);
        }
    }
}
