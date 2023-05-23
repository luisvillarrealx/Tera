using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;
using Tera_Web.Entities;
using Tera_Web.Models;

namespace Tera_Web.Controllers
{
    public class AuthController : Controller
    {
        //este se usa por un problema en las validaciones 
        UserLoginObj userLoginObj = new UserLoginObj();
        //esto es lo demas
        AuthModel authModel = new AuthModel();

        public ActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLoginObj userLoginObj)
        {
            try
            {
                if (string.IsNullOrEmpty(userLoginObj.userEmail))
                {
                    ModelState.AddModelError("userEmail", "Por favor, ingresa un correo electrónico.");
                }
                else if (!IsValidEmail(userLoginObj.userEmail))
                {
                    ModelState.AddModelError("userEmail", "Por favor, ingresa un correo electrónico válido.");
                }

                if (string.IsNullOrEmpty(userLoginObj.userPassword))
                {
                    ModelState.AddModelError("userPassword", "Por favor, ingresa una contraseña.");
                }
                if (ModelState.IsValid)
                {
                    var result = authModel.UserValidate(userLoginObj);
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
            }
            catch
            {
                return RedirectToAction(nameof(Login));
            }
            return View();
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }


        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}

