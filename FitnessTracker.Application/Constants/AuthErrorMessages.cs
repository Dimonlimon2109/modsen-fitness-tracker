
namespace FitnessTracker.Application.Constants
{
    public static class AuthErrorMessages
    {
        public const string EmailRequired = "Email обязателен.";
        public const string EmailInvalid = "Неверный формат email.";
        public const string EmailTooLong = $"Email не должен превышать 100 символов.";

        public const string PasswordRequired = "Пароль обязателен.";
        public const string PasswordTooShort = $"Пароль должен быть не короче 8 символов.";
        public const string PasswordTooLong = $"Пароль не должен превышать 30 символов.";
        public const string PasswordWeak = "Пароль должен содержать заглавную букву, цифру и специальный символ.";
    }
}
