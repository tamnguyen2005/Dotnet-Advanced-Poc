using Identity_RefreshToken.Interfaces;

namespace Identity_RefreshToken.Providers
{
    public class HashPassword : IHashPassword
    {
        public bool VerifyPassword(string hashedPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        string IHashPassword.HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}