using Identity_RefreshToken.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Identity_RefreshToken.Controllers
{
    [ApiController]
    [Route("[controller]/api/")]
    public class AuthController : ControllerBase
    {
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return NoContent();
        }
    }
}