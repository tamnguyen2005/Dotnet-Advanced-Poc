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

        public async Task<TokenResponse> Login(LoginRequest request)
        {
            if (!MockDatabase.Users.ContainsKey(request.UserName) ||
                MockDatabase.Users[request.UserName] != request.Password)
            {
                throw new UnauthorizedAccessException("Sai tài khoản hoặc mật khẩu !");
            }
            var accessToken = _tokenService.GenerateToken(request.UserName);
            var refreshToken = _tokenService.GenerateRefreshToken();
            MockDatabase.RefreshTokens.Add(new Models.UserRefreshToken()
            {
                UserName = request.UserName,
                Token = refreshToken,
                ExpiryDate = DateTime.Now.AddDays(7),
                IsRevoked = false
            });
            var response = new TokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return response;
        }

        public async Task<TokenResponse> Refresh(RefreshTokenRequest request)
        {
            var userName = _tokenService.GetUserNameFromExpiredToken(request.AccessToken);
            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("Token không hợp lệ !");
            }
            var users = MockDatabase.RefreshTokens.Where(u => u.UserName.Equals(userName));
            var user = users.FirstOrDefault();
            if (user != null)
            {
                throw new Exception("Người dùng không tồn tại !");
            }
            if (string.IsNullOrEmpty(user.Token) || !user.IsRevoked)
            {
                throw new Exception("Refresh token không tồn tại !");
            }
            if (user.Token != request.RefreshToken)
            {
                throw new Exception("Refresh token không khớp !");
            }
            if (user.ExpiryDate > DateTime.Now)
            {
                throw new Exception("Refresh token đã hết hạn sử dụng !");
            }
            var refreshToken = _tokenService.GenerateRefreshToken();
            var accessToken = _tokenService.GenerateToken(userName);
            MockDatabase.RefreshTokens.Clear();
            MockDatabase.RefreshTokens.Add(new Models.UserRefreshToken()
            {
                UserName = userName,
                IsRevoked = false,
                ExpiryDate = DateTime.Now.AddDays(7),
                Token = refreshToken
            });
            return new TokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}