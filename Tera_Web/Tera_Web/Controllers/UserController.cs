using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Tera_Web.Entities;
using Tera_Web.Models;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace Tera_Web.Controllers
{
    public class UserController : Controller
    {
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
        public ActionResult Register(UserObj userObj)
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

                if (string.IsNullOrEmpty(userObj.userPassword))
                {
                    ModelState.AddModelError("userPassword", "Por favor, ingresa una contraseña.");
                }

                if (ModelState.IsValid)
                {
                    // Los datos del formulario son válidos, realizar acciones adicionales, como guardar en la base de datos.
                    if (userModel.PostUsers(userObj) != string.Empty)
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
                    return View(userObj);
                }
            }
            catch
            {
                return RedirectToAction(nameof(View));
            }
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
        public ActionResult Delete(int id, bool confirmed)
        {
            if (confirmed)
            {
                userModel.DeleteUsers(id);
            }

            return RedirectToAction("List", "User");
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]

        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        //Validadores de campos
        private bool IsValidEmail(string email)
        {
            // Expresión regular para validar el formato del correo electrónico
            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            // Validar el formato del correo electrónico utilizando la expresión regular
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
