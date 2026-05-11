namespace QLPhongMay.BLL
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool Verify(string password, string passwordHash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordHash))
            {
                return false;
            }

            if (!IsBCryptHash(passwordHash))
            {
                return string.Equals(password, passwordHash);
            }

            try
            {
                return BCrypt.Net.BCrypt.Verify(password, passwordHash);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                return false;
            }
        }

        private static bool IsBCryptHash(string passwordHash)
        {
            return passwordHash.StartsWith("$2a$") ||
                   passwordHash.StartsWith("$2b$") ||
                   passwordHash.StartsWith("$2x$") ||
                   passwordHash.StartsWith("$2y$");
        }
    }
}
