using Identity_RefreshToken.DTOs;
using Identity_RefreshToken.Interfaces;

namespace Identity_RefreshToken.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHashPassword _hashPassword;
        private readonly ITokenServices _tokenService;

        public AuthService(IHashPassword hashPassword, ITokenServices tokenServices)
        {
            _hashPassword = hashPassword;
            _tokenService = tokenServices;
        }

        public Task<string> Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Refresh(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }
    }
}