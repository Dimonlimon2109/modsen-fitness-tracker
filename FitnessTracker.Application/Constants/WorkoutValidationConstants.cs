
namespace FitnessTracker.Application.Constants
{
    public class WorkoutValidationConstants
    {
        public const string TitleEmpty = "Название тренировки не может быть пустым.";
        public const string TitleTooLong = "Название тренировки не должно превышать 100 символов.";
        public const string DurationTooShort = "Длительность тренировки должна быть больше 0.";
        public const string CaloriesNonPositive = "Количество сожжённых калорий должно быть больше 0.";
        public const string DateInFuture = "Дата тренировки не может быть в будущем.";
        public const string ExercisesRequired = "Тренировка должна содержать хотя бы одно упражнение.";
        public const int TitleMaxLength = 100;
        public static readonly TimeSpan MinWorkoutDuration = TimeSpan.Zero;
        public const int MoreThanCaloriesNumber = 0;
        public const string ExerciseNameEmpty = "Название упражнения обязательно.";
        public const int ExerciseNameMaxLenght = 100;
        public const string SetsRequired = "Упражнение должно содержать хотя бы один подход.";
        public const int MoreThanRepsNumber = 0;
        public const string MoreThanRepsMessage = "Количество повторений должно быть больше 0.";
        public const int MoreThanWeight = 0;
        public const string WeightNonPositive = "Вес должен быть больше 0.";
    }
}
