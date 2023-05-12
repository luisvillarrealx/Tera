using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tera_Web.Entities;
using Tera_Web.Models;

namespace Tera_Web.Controllers
{
    public class RoleController : Controller
    {
        RoleObj roleObj = new RoleObj();
        RoleModel roleModel = new RoleModel();

        // GET: RolesController
        public ActionResult List()
        {
            List<RoleObj> _rol = new List<RoleObj>();
            _rol = roleModel.GetRoleList().ToList();
            return View(_rol);
        }

        // GET: RolesController/Create
        public ActionResult Register()
        {
            //ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre");
            return View();
        }



        // POST: RolesController/Create
        [HttpPost]
        public ActionResult Register(RoleObj roleObj)
        {
            string mensaje = ValidateRoles(roleObj);
            if (string.IsNullOrEmpty(mensaje))
            {
                roleModel.PostRoles(roleObj);
                return RedirectToAction("List", "Role");
                //else
                //{
                //    ErrorViewModel error = new ErrorViewModel();
                //    error.RequestId = "01";
                //    return View("Error", error);
                //}
            }
            else
            {
                ViewBag.MsjError = mensaje;
                return View("List");
            }
        }
        private string ValidateRoles(RoleObj roleObj)
        {

            if (string.IsNullOrEmpty(roleObj.roleName))
            {
                return "Cree un nombre para el Rol";
            }

            return string.Empty;
        }

        // GET: RolesController/Edit/5
        [HttpGet]
        public ActionResult EditRole(int id)
        {

            roleObj = roleModel.GetRole(id);

            if (roleObj == null)
            {
                return NotFound();
            }
            //ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre", articulos.CodigoProveedor);
            return View(roleObj);
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        public ActionResult EditRole(RoleObj roleObj)
        {
            if (ModelState.IsValid)
            {
                roleModel.PUTRoles(roleObj);
                return RedirectToAction("List", "Role");
            }
            //ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre", articulos.CodigoProveedor);
            return View(roleObj);
        }

        // GET: RolesController/Delete/5
        public ActionResult DeleteRole(int id)
        {

            if (id == null)
            {
                return BadRequest();
            }
            roleModel.DeleteRoles(id);
            if (id == null)
            {
                return NotFound();
            }
            return RedirectToAction("List", "Role");
        }

        // POST: ProductosController/Delete/5
        [HttpPost]

        public ActionResult DeleteRole(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }
       
    }
}
