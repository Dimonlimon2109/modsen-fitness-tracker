
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces.Repositories;

namespace FitnessTracker.Infrastructure.Repositories
{
    public class WorkoutRepository(FitnessDbContext context) : Repository<WorkoutEntity>(context), IWorkoutRepository
    {
    }
}
