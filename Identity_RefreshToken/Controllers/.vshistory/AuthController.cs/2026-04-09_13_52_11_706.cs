using Identity_RefreshToken.DTOs;
using Identity_RefreshToken.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity_RefreshToken.Controllers
{
    [ApiController]
    [Route("[controller]/api/")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var tokenResponse = await _authService.Login(request);
            return Ok(tokenResponse);
        }

        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            return NoContent();
        }
    }
}