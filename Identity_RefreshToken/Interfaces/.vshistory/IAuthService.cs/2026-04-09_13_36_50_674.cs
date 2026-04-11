using Identity_RefreshToken.DTOs;

namespace Identity_RefreshToken.Interfaces
{
    public interface IAuthService
    {
        Task Login(LoginRequest request);

        Task Refresh(RefreshTokenRequest request);
    }
}