using Microsoft.AspNetCore.Mvc;

namespace Tera_API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Reset()
        {
            return View();
        }
    }
}
