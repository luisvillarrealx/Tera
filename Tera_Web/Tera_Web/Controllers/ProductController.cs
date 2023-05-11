using Microsoft.AspNetCore.Mvc;

namespace Tera_Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
