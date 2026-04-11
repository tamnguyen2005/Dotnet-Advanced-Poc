using Identity_RefreshToken.Interfaces;

namespace Identity_RefreshToken.Providers
{
    public class HashPassword : IHashPassword
    {
        public bool VerifyPassword(string hashedPassword, string password)
        {
            throw new NotImplementedException();
        }

        string IHashPassword.HashPassword(string password)
        {
            throw new NotImplementedException();
        }
    }
}