using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Tera_Web.Entities;
using Tera_Web.Models;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

namespace Tera_Web.Controllers
{
    public class UserController : Controller
    {
        UserObj credentialObj = new UserObj();
        UserModel credentialModel = new UserModel();

        public ActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult List()
        {
            List<UserObj> _user = new List<UserObj>();
            _user = credentialModel.GetUser().ToList();
            //if (HttpContext.Session.GetInt32("TokenState") == 0)
            return View(_user);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(UserObj credentialObj)
        {
            try
            {
                if (credentialModel.PostUsers(credentialObj) != string.Empty)
                    return RedirectToAction(nameof(List));
                else
                {
                    ErrorViewModel error = new ErrorViewModel();
                    error.RequestId = "01";
                    return View("Error", error);
                }
            }
            catch
            {
                return RedirectToAction(nameof(View));
            }
        }
    }
}
