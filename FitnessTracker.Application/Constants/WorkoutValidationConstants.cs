
namespace FitnessTracker.Application.Constants
{
    public class WorkoutValidationConstants
    {
        public const string TitleEmpty = "Workout title cannot be empty.";
        public const string TitleTooLong = "Workout title must not exceed 100 characters.";
        public const string DurationTooShort = "Workout duration must be greater than 0.";
        public const string CaloriesNonPositive = "Calories burned must be greater than 0.";
        public const string DateInFuture = "Workout date cannot be in the future.";
        public const string ExercisesRequired = "Workout must contain at least one exercise.";
        public const int TitleMaxLength = 100;
        public static readonly TimeSpan MinWorkoutDuration = TimeSpan.Zero;
        public const int MoreThanCaloriesNumber = 0;
        public const string ExerciseNameEmpty = "Exercise name is required.";
        public const int ExerciseNameMaxLength = 100;
        public const string SetsRequired = "Exercise must contain at least one set.";
        public const int MoreThanRepsNumber = 0;
        public const string MoreThanRepsMessage = "Number of repetitions must be greater than 0.";
        public const int MoreThanWeight = 0;
        public const string WeightNonPositive = "Weight must be greater than 0.";
    }
}
