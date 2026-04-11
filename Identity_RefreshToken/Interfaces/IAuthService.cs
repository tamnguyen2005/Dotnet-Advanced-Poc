using Identity_RefreshToken.DTOs;

namespace Identity_RefreshToken.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponse> Login(LoginRequest request);

        Task<TokenResponse> Refresh(RefreshTokenRequest request);
    }
}