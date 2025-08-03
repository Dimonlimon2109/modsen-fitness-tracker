

namespace FitnessTracker.Application.Exceptions.Workouts
{
    public class WorkoutForbiddenException(string message) : ForbiddenException(message)
    {
    }
}
