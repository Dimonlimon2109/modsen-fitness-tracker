
namespace FitnessTracker.Application.Constants
{
    public static class AuthErrorMessages
    {
        public const string EmailRequired = "Email is required.";
        public const string EmailInvalid = "Invalid email format.";
        public const string EmailTooLong = "Email must not exceed 100 characters.";

        public const string PasswordRequired = "Password is required.";
        public const string PasswordTooShort = "Password must be at least 8 characters long.";
        public const string PasswordTooLong = "Password must not exceed 30 characters.";
        public const string PasswordWeak = "Password must contain an uppercase letter, a digit, and a special character.";

    }
}
