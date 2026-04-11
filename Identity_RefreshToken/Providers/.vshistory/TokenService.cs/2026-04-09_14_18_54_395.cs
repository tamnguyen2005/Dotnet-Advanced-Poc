using Identity_RefreshToken.Interfaces;

namespace Identity_RefreshToken.Providers
{
    public class TokenService : ITokenServices
    {
        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(string userName)
        {
            throw new NotImplementedException();
        }

        public string GetUserNameFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}