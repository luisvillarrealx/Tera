﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tera_Web.Entities;
using Tera_Web.Filters;
using Tera_Web.Models;

namespace Tera_Web.Controllers
{
    [FilterSessionValidation]
    [FilterSession]
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
            var userUnitListCombo = new List<SelectListItem>();

            userUnitListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona una unidad" });
            userUnitListCombo.Add(new SelectListItem { Value = "Kg", Text = "Kilogramo" });
            userUnitListCombo.Add(new SelectListItem { Value = "G", Text = "Gramo" });
            userUnitListCombo.Add(new SelectListItem { Value = "Gal", Text = "Galón" });
            userUnitListCombo.Add(new SelectListItem { Value = "Unidad", Text = "Unidad" });

            ViewBag.ComboUnit = userUnitListCombo;

            var categoryIdCombo = productModel.ComboBoxCategory();
            var categoryIdListCombo = new List<SelectListItem>();

            categoryIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona una Categoria" });
            foreach (var item in categoryIdCombo)
                categoryIdListCombo.Add(new SelectListItem { Value = item.categoryId.ToString(), Text = item.CategoryName });
            ViewBag.CombocategoryId = categoryIdListCombo.AsEnumerable();
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
                return RedirectToAction(nameof(List));
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
            var userUnitListCombo = new List<SelectListItem>();

            userUnitListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona una unidad" });
            userUnitListCombo.Add(new SelectListItem { Value = "Kg", Text = "Kilogramo" });
            userUnitListCombo.Add(new SelectListItem { Value = "G", Text = "Gramo" });
            userUnitListCombo.Add(new SelectListItem { Value = "Gal", Text = "Galón" });
            userUnitListCombo.Add(new SelectListItem { Value = "Unidad", Text = "Unidad" });

            ViewBag.ComboUnit = userUnitListCombo;
            productObj = productModel.GetProduct(id);

            var categoryIdCombo = productModel.ComboBoxCategory();
            var categoryIdListCombo = new List<SelectListItem>();

            categoryIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona una Categoria" });
            foreach (var item in categoryIdCombo)
                categoryIdListCombo.Add(new SelectListItem { Value = item.categoryId.ToString(), Text = item.CategoryName });
            ViewBag.CombocategoryId = categoryIdListCombo.AsEnumerable();
            return View(productObj);
        }

        // POST: ProductosController/Edit/5
        [HttpPost]

        public ActionResult EditProduct(ProductObj productObj, IFormCollection collection)
        {


            try
            {

                productModel.PutProduct(productObj);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }

        }

        // GET: ProductosController/Delete/5
        public ActionResult DeleteProduct(int id)
        {

            productModel.DeleteProduct(id);
            return RedirectToAction("List", "Product");

        }

        // POST: ProductosController/Delete/5
        [HttpPost]

        public ActionResult DeleteProduct(int id, IFormCollection collection)
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
