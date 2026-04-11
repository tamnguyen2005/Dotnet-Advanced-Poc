using Identity_RefreshToken.Context;
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

        public Task<TokenResponse> Login(LoginRequest request)
        {
            if (!MockDatabase.Users.ContainsKey(request.UserName) ||
                MockDatabase.Users[request.UserName] != request.Password)
            {
                throw new UnauthorizedAccessException("Sai tài khoản hoặc mật khẩu !");
            }
            var accessToken = _tokenService.GenerateToken(request.UserName);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var response = new TokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public Task<TokenResponse> Refresh(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }
    }
}