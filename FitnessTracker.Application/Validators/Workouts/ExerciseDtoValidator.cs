using FitnessTracker.Application.Constants;
using FitnessTracker.Application.Contracts.DTOs;
using FitnessTracker.Application.Contracts.Requests;
using FluentValidation;
using FluentValidation.Validators;

namespace FitnessTracker.Application.Validators.Workouts
{
    public class ExerciseDtoValidator : AbstractValidator<ExerciseDTO>
    {
        public ExerciseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(WorkoutValidationConstants.ExerciseNameEmpty)
                .MaximumLength(WorkoutValidationConstants.ExerciseNameMaxLength);

            RuleFor(x => x.Sets)
                .NotEmpty().WithMessage(WorkoutValidationConstants.SetsRequired);

            RuleForEach(x => x.Sets)
                .SetValidator(new SetDtoValidator());
        }
    }
}