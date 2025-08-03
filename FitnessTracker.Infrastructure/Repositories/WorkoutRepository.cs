
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Enums;
using FitnessTracker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repositories
{
    public class WorkoutRepository(FitnessDbContext context) : Repository<WorkoutEntity>(context), IWorkoutRepository
    {
        public async Task<List<WorkoutEntity>> GetAllWorkoutsWithFiltersAsync(
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
            )
        {
            var query = _dbSet
                .Where(w => w.User.Id == userId)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(w => w.Title.Contains(title));
            }

            // Фильтрация по типу
            if (type.HasValue)
            {
                query = query.Where(w => w.Type == type.Value);
            }

            // Фильтрация по датам
            if (startDate.HasValue)
            {
                query = query.Where(w => w.WorkoutDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(w => w.WorkoutDate <= endDate.Value);
            }

            // Фильтрация по продолжительности
            if (minDuration.HasValue)
            {
                query = query.Where(w => w.Duration >= minDuration.Value);
            }

            if (maxDuration.HasValue)
            {
                query = query.Where(w => w.Duration <= maxDuration.Value);
            }

            if (!string.IsNullOrWhiteSpace(sortBy) && !string.IsNullOrWhiteSpace(order))
            {
                query = (sortBy?.ToLower(), order.ToLower()) switch
                {
                    ("calories", "asc") => query.OrderBy(w => w.CaloriesBurned),
                    ("calories", "desc") => query.OrderByDescending(w => w.CaloriesBurned),
                    ("date", "desc") => query.OrderByDescending(w => w.WorkoutDate),
                    ("date", "asc") => query.OrderBy(w => w.WorkoutDate),
                    _ => query.OrderBy(w => w.Id)
                };
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task<int> GetTotalPagesAsync(
            int userId,
            string? title,
            WorkoutType? type,
            DateTime? startDate,
            DateTime? endDate,
            TimeSpan? minDuration,
            TimeSpan? maxDuration,
            int pageSize,
            CancellationToken ct = default
            )
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(w => w.User.Id == userId)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(w => w.Title.Contains(title));
            }

            // Фильтрация по типу
            if (type.HasValue)
            {
                query = query.Where(w => w.Type == type.Value);
            }

            // Фильтрация по датам
            if (startDate.HasValue)
            {
                query = query.Where(w => w.WorkoutDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(w => w.WorkoutDate <= endDate.Value);
            }

            // Фильтрация по продолжительности
            if (minDuration.HasValue)
            {
                query = query.Where(w => w.Duration >= minDuration.Value);
            }

            if (maxDuration.HasValue)
            {
                query = query.Where(w => w.Duration <= maxDuration.Value);
            }

            int count = await query.CountAsync(ct);
            return (int)Math.Ceiling((double)count / pageSize);
        }
    }
}
