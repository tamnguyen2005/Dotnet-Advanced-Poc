using Microsoft.AspNetCore.Mvc;

namespace Identity_RefreshToken.Controllers
{
    [ApiController]
    [Route("[controller]/api/")]
    public class AuthController : ControllerBase
    {
        public Task<IActionResult> Login()
        {
        }
    }
}