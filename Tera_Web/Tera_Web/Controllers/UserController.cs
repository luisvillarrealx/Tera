using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Tera_Web.Entities;
using Tera_Web.Models;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Tera_Web.Controllers
{
    public class UserController : Controller
    {
        //este se usa por un problema en las validaciones 
        UserEPObj userEPObj = new UserEPObj();
        //esto es lo demas
        UserObj userObj = new UserObj();
        UserModel userModel = new UserModel();


        public IActionResult List()
        {
            List<UserObj> _user = new List<UserObj>();
            _user = userModel.GetUserList().ToList();
            //if (HttpContext.Session.GetInt32("TokenState") == 0)
            return View(_user);
        }

        [HttpGet]
        public ActionResult Register()
        {
            //    var Roles = userModel.CheckRoles();
            //    var datos = new List<SelectListItem>();

            //    datos.Add(new SelectListItem { Value = "0", Text = "Selecciona" });
            //    foreach (var item in Roles)
            //        datos.Add(new SelectListItem { Value = item.roleId.ToString(), Text = item.roleName });

            //    ViewBag.ComboRoles = datos;
            return View();
        }


        [HttpPost]
        public ActionResult Register(UserEPObj userEPObj)
        {
            try
            {
                if (string.IsNullOrEmpty(userEPObj.userEmail))
                {
                    ModelState.AddModelError("userEmail", "Por favor, ingresa un correo electrónico.");
                }
                else if (!IsValidEmail(userEPObj.userEmail))
                {
                    ModelState.AddModelError("userEmail", "Por favor, ingresa un correo electrónico válido.");
                }

                if (string.IsNullOrEmpty(userEPObj.userPassword))
                {
                    ModelState.AddModelError("userPassword", "Por favor, ingresa una contraseña.");
                }

                if (ModelState.IsValid)
                {
                    // Los datos del formulario son válidos, realizar acciones adicionales, como guardar en la base de datos.
                    if (userModel.PostUsers(userEPObj) != string.Empty)
                        return RedirectToAction(nameof(List));
                    else
                    {
                        ErrorViewModel error = new ErrorViewModel();
                        error.RequestId = "01";
                        return View("Error", error);
                    }
                }
                else
                {
                    // Si hay errores de validación, vuelve a mostrar el formulario con los mensajes de error.
                    return View(userEPObj);
                }
            }
            catch
            {
                return RedirectToAction(nameof(View));
            }
        }

        [HttpPost]
        public ActionResult EmailExists(string validateEmailExists)
        {
            return Json(userModel.EmailExists(validateEmailExists));
        }

        public ActionResult EditUser(int id)
        {
            userObj = userModel.GetUser(id);
            return View(userObj);
        }

        [HttpPost]
        public ActionResult EditUser(UserObj userObj)
        {
            try
            {
                if (string.IsNullOrEmpty(userObj.userEmail))
                {
                    ModelState.AddModelError("userEmail", "Por favor, ingresa un correo electrónico.");
                }
                else if (!IsValidEmail(userObj.userEmail))
                {
                    ModelState.AddModelError("userEmail", "Por favor, ingresa un correo electrónico válido.");
                }

                if (string.IsNullOrEmpty(userObj.userGovId))
                {
                    ModelState.AddModelError("userGovId", "Por favor, ingresa la cédula.");
                }
                else if (userObj.userGovId.Length != 9)
                {
                    ModelState.AddModelError("userGovId", "La cédula debe tener 9 dígitos.");
                }

                if (ModelState.IsValid)
                {
                    userModel.PutUsers(userObj);
                    return RedirectToAction("List", "User");
                }
                else
                {
                    return View(userObj);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        [HttpPost]
        public ActionResult DeleteUser(int id, bool confirmed)
        {
            if (confirmed)
            {
                userModel.DeleteUsers(id);
            }

            return RedirectToAction("List", "User");
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        public ActionResult ChangeUserActive(long id)
        {
            userModel.ChangeUserActive(id);
            return Json("Ok");
        }


        //Validadores de campos
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
    }
}