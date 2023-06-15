using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tera_Web.Entities;
using Tera_Web.Models;

namespace Tera_Web.Controllers
{
    public class CategoryController : Controller
    {
        CategoryObj CategoryObj = new CategoryObj();
        CategoryModel CategoryModel = new CategoryModel();

        // GET: RolesController
        public ActionResult List()
        {
            List<CategoryObj> _categoryObj = new List<CategoryObj>();
            _categoryObj = CategoryModel.GetCategoryList().ToList();
            return View(_categoryObj);
        }

        // GET: RolesController/Create
        public ActionResult Register()
        {
            //ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre");
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        public ActionResult Register(CategoryObj CategoryObj)
        {
            string mensaje = ValidateRoles(CategoryObj);
            if (string.IsNullOrEmpty(mensaje))
            {
                CategoryModel.PostCategory(CategoryObj);
                return RedirectToAction("List", "Category");
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
        private string ValidateRoles(CategoryObj CategoryObj)
        {
            if (string.IsNullOrEmpty(CategoryObj.CategoryName))
            {
                return "Cree un nombre para el Rol";
            }
            return string.Empty;
        }

        // GET: RolesController/Edit/5
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            CategoryObj = CategoryModel.GetCategory(id);

            if (CategoryObj == null)
            {
                return NotFound();
            }
            //ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre", articulos.CodigoProveedor);
            return View(CategoryObj);
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        public ActionResult EditCategory(CategoryObj CategoryObj)
        {
            if (ModelState.IsValid)
            {
                CategoryModel.PUTCategory(CategoryObj);
                return RedirectToAction("List", "Category");
            }
            //ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre", articulos.CodigoProveedor);
            return View(CategoryObj);
        }

        // GET: RolesController/Delete/5
        public ActionResult DeleteCategory(int categoryId)
        {
            if (categoryId == null)
            {
                return BadRequest();
            }
            CategoryModel.DeleteCategory(categoryId);
            if (categoryId == null)
            {
                return NotFound();
            }
            return RedirectToAction("List", "Category");
        }

        // POST: ProductosController/Delete/5
        [HttpPost]
        public ActionResult DeleteCategory(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction("List", "Category");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CategoryExist(string CategoryName)
        {
            // Validar si el correo electrónico existe
            bool categoryExist = CategoryModel.CategoryExist(CategoryName);

            return Json(categoryExist);
        }
    }
}
