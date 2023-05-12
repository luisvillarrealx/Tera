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
        public ActionResult Create()
        {
            //ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre");
            return View();
        }



        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleObj roleObj)
        {
            string mensaje = ValidateRoles(roleObj);
            if (string.IsNullOrEmpty(mensaje))
            {
                roleModel.PostRoles(roleObj);
                return RedirectToAction("Index", "Roles");
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
                return View("Create");
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
        public ActionResult Edit(int id)
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleObj roleObj)
        {
            if (ModelState.IsValid)
            {
                roleModel.PUTRoles(roleObj);
                return RedirectToAction("Index", "Roles");
            }
            //ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre", articulos.CodigoProveedor);
            return View(roleObj);
        }

        // GET: RolesController/Delete/5
        public ActionResult Delete(int id)
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
            return RedirectToAction("Index", "Roles");
        }

        // POST: ProductosController/Delete/5
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
