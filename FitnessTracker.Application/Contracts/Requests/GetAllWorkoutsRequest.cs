
using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Application.Contracts.Requests
{
    public record GetAllWorkoutsRequest
        (
        string? Title,
        WorkoutType? Type,
        DateTime? StartDate,
        DateTime? EndDate,
        TimeSpan? MinDuration,
        TimeSpan? MaxDuration,
        int Page,
        int PageSize,
        string? SortBy,
        string Order
        );
}
