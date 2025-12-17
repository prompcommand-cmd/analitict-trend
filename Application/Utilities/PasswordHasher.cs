using BCrypt.Net;

namespace Application.Utilities
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // workFactor = 10
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
