using Microsoft.AspNetCore.Mvc;
using Tera_Web.Entities;
using Tera_Web.Models;

namespace Tera_Web.Controllers
{
    public class ProductController : Controller
    {
        //private readonly ILogger<Usuario> _logger;

        ProductObj productObj = new ProductObj();
        ProductModel productModel = new ProductModel();
        // GET: ProductosController

        // GET: ProductosController
        public ActionResult List()
        {

            List<ProductObj> _Productos = new List<ProductObj>();
            _Productos = productModel.GetProductList().ToList();
            return View(_Productos);
        }

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

        // POST: ProductosController/Create
        [HttpPost]
        public ActionResult Register(ProductObj productObj)
        {
            //string mensaje = ValidateProductos(prodC);
            //if (string.IsNullOrEmpty(mensaje))
            //{
            try
            {

                productModel.PostProduct(productObj);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            //}
            //else
            //{
            //    ViewBag.MsjError = mensaje;
            //    return View("Create");
            //}
        }


        // GET: ProductosController/Edit/5
        public ActionResult EditProduct(int id)
        {

            productObj = productModel.GetProduct(id);
            return View(productObj);
        }

        // POST: ProductosController/Edit/5
        [HttpPost]

        public ActionResult EditProduct(ProductObj productObj, IFormCollection collection)
        {


            try
            {

                productModel.PutProduct(productObj);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: ProductosController/Delete/5
        public ActionResult Delete(int id)
        {

            productModel.DeleteProduct(id);
            return RedirectToAction("Index", "Productos");

        }

        // POST: ProductosController/Delete/5
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
    }
}
