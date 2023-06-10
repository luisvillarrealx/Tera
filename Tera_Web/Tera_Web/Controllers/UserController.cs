using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Tera_Web.Entities;
using Tera_Web.Filters;
using Tera_Web.Models;

namespace Tera_Web.Controllers
{
    [FilterSession]
    public class UserController : Controller
    {
        //Objs
        UserObj userObj = new UserObj();
        //models
        UserModel userModel = new UserModel();
        AuthModel authModel = new AuthModel();


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
            try
            {
                var userRoleIdCombo = userModel.ComboBoxRoles();
                var userRoleIdListCombo = new List<SelectListItem>();

                userRoleIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona un Rol" });
                foreach (var item in userRoleIdCombo)
                    userRoleIdListCombo.Add(new SelectListItem { Value = item.roleId.ToString(), Text = item.roleName });

                // SiteCombo
                var userSiteIdCombo = userModel.ComboBoxSites();
                var userSiteIdListCombo = new List<SelectListItem>();

                userSiteIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona una sede" });
                foreach (var item in userSiteIdCombo)
                    userSiteIdListCombo.Add(new SelectListItem { Value = item.siteId.ToString(), Text = item.siteName });

                ViewBag.CombouserRoleId = userRoleIdListCombo.AsEnumerable();
                ViewBag.CombouserSiteId = userSiteIdListCombo.AsEnumerable(); // Convertir a IEnumerable<SelectListItem>
                return View();
            }
            catch
            {
                // Manejo de errores
                return View();
            }
        }


        [HttpPost]
        public IActionResult Register(UserObj userObj)
        {
            try
            {
                // Generar una contraseña aleatoria de máximo 10 caracteres
                string password = GenerateRandomPassword(10);

                userObj.userPassword = password;

                // Los datos del formulario son válidos, realizar acciones adicionales, como guardar en la base de datos.
                if (userModel.PostUsers(userObj) != string.Empty)
                {
                    authModel.ResetPassword(userObj);
                    return RedirectToAction(nameof(List));
                }
                else
                {
                    ErrorViewModel error = new ErrorViewModel();
                    error.RequestId = "01";
                    return View("Error", error);
                }
            }
            catch
            {
                return RedirectToAction(nameof(List));
            }
        }

        [HttpPost]
        public ActionResult EmailExists(string userEmail)
        {
            // Validar si el correo electrónico existe
            bool emailExists = userModel.EmailExists(userEmail);

            return Json(emailExists);
        }


        private string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                int index = random.Next(validChars.Length);
                password.Append(validChars[index]);
            }

            return password.ToString();
        }

        public ActionResult EditUser(int id)
        {
            try
            {
                userObj = userModel.GetUser(id);
                var userRoleIdCombo = userModel.ComboBoxRoles();
                var userRoleIdListCombo = new List<SelectListItem>();

                userRoleIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona un Rol" });
                foreach (var item in userRoleIdCombo)
                    userRoleIdListCombo.Add(new SelectListItem { Value = item.roleId.ToString(), Text = item.roleName });

                // SiteCombo
                var userSiteIdCombo = userModel.ComboBoxSites();
                var userSiteIdListCombo = new List<SelectListItem>();

                userSiteIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona una sede" });
                foreach (var item in userSiteIdCombo)
                    userSiteIdListCombo.Add(new SelectListItem { Value = item.siteId.ToString(), Text = item.siteName });

                ViewBag.CombouserRoleId = userRoleIdListCombo;
                ViewBag.CombouserSiteId = userSiteIdListCombo;
                return View(userObj);
            }
            catch
            {
                var userRoleIdCombo = userModel.ComboBoxRoles();
                var userRoleIdListCombo = new List<SelectListItem>();

                userRoleIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona un Rol" });
                foreach (var item in userRoleIdCombo)
                    userRoleIdListCombo.Add(new SelectListItem { Value = item.roleId.ToString(), Text = item.roleName });

                // SiteCombo
                var userSiteIdCombo = userModel.ComboBoxSites();
                var userSiteIdListCombo = new List<SelectListItem>();

                userSiteIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona una sede" });
                foreach (var item in userSiteIdCombo)
                    userSiteIdListCombo.Add(new SelectListItem { Value = item.siteId.ToString(), Text = item.siteName });

                ViewBag.CombouserRoleId = userRoleIdListCombo;
                ViewBag.CombouserSiteId = userSiteIdListCombo;
                return View(userObj);
            }

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

        public ActionResult UserProfile()
        {

            try
            {
                userObj = userModel.GetUser(Convert.ToInt32(HttpContext.Session.GetString("userId")));
                userModel.PutUsers(userObj);
                var userRoleIdCombo = userModel.ComboBoxRoles();
                var userRoleIdListCombo = new List<SelectListItem>();

                userRoleIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona un Rol" });
                foreach (var item in userRoleIdCombo)
                    userRoleIdListCombo.Add(new SelectListItem { Value = item.roleId.ToString(), Text = item.roleName });

                // SiteCombo
                var userSiteIdCombo = userModel.ComboBoxSites();
                var userSiteIdListCombo = new List<SelectListItem>();

                userSiteIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona una sede" });
                foreach (var item in userSiteIdCombo)
                    userSiteIdListCombo.Add(new SelectListItem { Value = item.siteId.ToString(), Text = item.siteName });

                ViewBag.CombouserRoleId = userRoleIdListCombo;
                ViewBag.CombouserSiteId = userSiteIdListCombo;
                return View(userObj);

            }
            catch
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult UserProfile(UserObj userObj)
        {
            try
            {
                userModel.PutUsers(userObj);
                return RedirectToAction("List", "User");
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
        public ActionResult ResetPassword(int id)
        {
            userObj.userId = id;
            return View(userObj);
        }
        [HttpPost]
        public ActionResult ResetPassword(UserObj userObj)
        {
            userModel.ResetPassword(userObj);
            return RedirectToAction("UserProfile", "User");
        }
    }
}