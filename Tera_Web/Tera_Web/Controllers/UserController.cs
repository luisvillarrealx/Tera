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
                if (userModel.PostUsers(userObj) != string.Empty)
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
                userModel.PutUsers(userObj);
                return RedirectToAction("List", "User");
            }
            catch
            {
                //var Roles = use.ConsultarRoles();
                //var datos = new List<SelectListItem>();

                //datos.Add(new SelectListItem { Value = "0", Text = "Selecciona" });
                //foreach (var item in Roles)
                //    datos.Add(new SelectListItem { Value = item.IdRol.ToString(), Text = item.Roles });

                return View();
            }
        }
        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            userModel.DeleteUsers(id);
            return RedirectToAction("List", "User");
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
    }
}
