
using FitnessTracker.Application.Constants;
using FitnessTracker.Application.Contracts.Requests;
using FluentValidation;

namespace FitnessTracker.Application.Validators.Auth
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(AuthErrorMessages.EmailRequired)
                .EmailAddress().WithMessage(AuthErrorMessages.EmailInvalid)
                .MaximumLength(AuthValidationConstants.EmailMaxLength)
                .WithMessage(AuthErrorMessages.EmailTooLong);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(AuthErrorMessages.PasswordRequired)
                .MinimumLength(AuthValidationConstants.PasswordMinLength)
                .WithMessage(AuthErrorMessages.PasswordTooShort)
                .MaximumLength(AuthValidationConstants.PasswordMaxLength)
                .WithMessage(AuthErrorMessages.PasswordTooLong)
                .Matches(AuthValidationConstants.PasswordRegex)
                .WithMessage(AuthErrorMessages.PasswordWeak);
        }
    }
}
