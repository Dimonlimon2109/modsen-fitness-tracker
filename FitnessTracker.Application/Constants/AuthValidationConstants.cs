

    namespace FitnessTracker.Application.Constants
    {
        public static class AuthValidationConstants
        {
            public static int EmailMaxLength = 100;
            public static int PasswordMinLength = 8;
            public static int PasswordMaxLength = 30;
            public static string PasswordRegex = @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]).*$";
        }
    }
