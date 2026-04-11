using Microsoft.AspNetCore.Mvc;

namespace Identity_RefreshToken.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
