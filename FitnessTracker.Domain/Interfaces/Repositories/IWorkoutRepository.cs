
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Domain.Interfaces.Repositories
{
    public interface IWorkoutRepository : IRepository<WorkoutEntity>
    {
        Task<List<WorkoutEntity>?> GetAllWorkoutsWithFiltersAsync(
            int userId,
            string? title,
            WorkoutType? type,
            DateTime? startDate,
            DateTime? endDate,
            TimeSpan? minDuration,
            TimeSpan? maxDuration,
            int page,
            int pageSize,
            string? sortBy,
            string order,
            CancellationToken ct = default
            );
        Task<int> GetTotalPagesAsync(
            int userId,
            string? title,
            WorkoutType? type,
            DateTime? startDate,
            DateTime? endDate,
            TimeSpan? minDuration,
            TimeSpan? maxDuration,
            int pageSize,
            CancellationToken ct = default
            );
    }
}
