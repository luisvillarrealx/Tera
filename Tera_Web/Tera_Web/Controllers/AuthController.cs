using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;
using Tera_Web.Entities;
using Tera_Web.Models;

namespace Tera_Web.Controllers
{
    public class AuthController : Controller
    {
        //Objs
        UserObj userObj = new UserObj();
        //Models
        AuthModel authModel = new AuthModel();

        public ActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserObj userObj)
        {
            try
            {
                var result = authModel.UserValidate(userObj);
                if (result != null)
                {

                    HttpContext.Session.SetString("userGovId", result.userGovId.ToString());
                    HttpContext.Session.SetString("userEmail", result.userEmail);
                    HttpContext.Session.SetString("userName", result.userName);
                    HttpContext.Session.SetString("userFirstSurname", result.userFirstSurname);
                    HttpContext.Session.SetString("userSecondSurname", result.userSecondSurname);
                    HttpContext.Session.SetString("userId", result.userId.ToString());



                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.MsjError = "Verifique sus credenciales de acceso!";
                    return View("Login");
                }

            }
            catch
            {
                return RedirectToAction(nameof(Login));
            }
        }
        [HttpGet]
        public ActionResult Reset()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Reset(UserObj userObj)
        {
            //try
            //{
            authModel.ResetPassword(userObj);
            return RedirectToAction("Login", "Auth");
            //}
            //catch (Exception ex)
            //{
            //    RegistrarLog(ex, ControllerContext);
            //    return View("Index");
            //}
            //return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}

