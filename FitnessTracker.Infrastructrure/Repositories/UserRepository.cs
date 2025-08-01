
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces.Repositories;

namespace FitnessTracker.Infrastructrure.Repositories
{
    public class UserRepository(FitnessDbContext context) : Repository<UserEntity>(context), IUserRepository
    {
    }
}
