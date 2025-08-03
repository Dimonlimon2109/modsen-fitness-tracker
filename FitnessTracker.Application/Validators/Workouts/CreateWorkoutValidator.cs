
using FitnessTracker.Application.Constants;
using FitnessTracker.Application.Contracts.Requests;
using FluentValidation;

namespace FitnessTracker.Application.Validators.Workouts
{
    public class CreateWorkoutValidator : AbstractValidator<CreateWorkoutRequest>
    {
        public CreateWorkoutValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(WorkoutValidationConstants.TitleEmpty)
                .MaximumLength(WorkoutValidationConstants.TitleMaxLength).WithMessage(WorkoutValidationConstants.TitleTooLong);

            RuleFor(x => x.Duration)
                .GreaterThan(WorkoutValidationConstants.MinWorkoutDuration).WithMessage(WorkoutValidationConstants.DurationTooShort);

            RuleFor(x => x.CaloriesBurned)
                .GreaterThan(WorkoutValidationConstants.MoreThanCaloriesNumber).WithMessage(WorkoutValidationConstants.CaloriesNonPositive);

            RuleFor(x => x.WorkoutDate)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage(WorkoutValidationConstants.DateInFuture);

            RuleFor(x => x.Exercises)
                .NotEmpty().WithMessage(WorkoutValidationConstants.ExercisesRequired);

            RuleForEach(x => x.Exercises)
                .SetValidator(new ExerciseDtoValidator());
        }
    }
}
