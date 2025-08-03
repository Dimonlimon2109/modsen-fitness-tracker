using FitnessTracker.Application.Constants;
using FitnessTracker.Application.Contracts.DTOs;
using FluentValidation;
using FluentValidation.Validators;

namespace FitnessTracker.Application.Validators.Workouts
{
    public class SetDtoValidator : AbstractValidator<SetDTO>
    {
        public SetDtoValidator()
        {
            RuleFor(x => x.Reps)
                .GreaterThan(WorkoutValidationConstants.MoreThanRepsNumber).WithMessage(WorkoutValidationConstants.MoreThanRepsMessage);

            RuleFor(x => x.Weight)
                .GreaterThanOrEqualTo(WorkoutValidationConstants.MoreThanWeight).WithMessage(WorkoutValidationConstants.WeightNonPositive);
        }
    }
}