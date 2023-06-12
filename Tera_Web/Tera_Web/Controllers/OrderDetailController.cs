using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tera_Web.Entities;
using Tera_Web.Filters;
using Tera_Web.Models;

namespace Tera_Web.Controllers
{
    [FilterSession]
    public class OrderDetailController : Controller
    {
        OrderDetailObj orderDetailsObj = new OrderDetailObj();
        OrderDetailModel orderDetailsModel = new OrderDetailModel();

        // GET: Order/List
        [FilterSessionValidation]
        public IActionResult List()
        {
            List<OrderDetailObj> orders = orderDetailsModel.GetOrderDetailsList();
            return View(orders);
        }

        public IActionResult ListOrderDetailsUser(int orderId)
        {
            List<OrderDetailObj> orders = orderDetailsModel.GetOrderDetailsListUser(orderId);
            return View(orders);
        }

        // GET: Order/InsertOrderDetails
        public ActionResult InsertOrderDetails(int id)
        {
            try
            {
                orderDetailsObj.orderId = id;
                var ProductIdCombo = orderDetailsModel.ComboBoxProduct();
                var ProductIdListCombo = new List<SelectListItem>();

                ProductIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona un Producto" });
                foreach (var item in ProductIdCombo)
                    ProductIdListCombo.Add(new SelectListItem { Value = item.productId.ToString(), Text = item.productName });

                ViewBag.ComboProductId = ProductIdListCombo.AsEnumerable();
                return View(orderDetailsObj);
            }
            catch
            {
                var ProductIdCombo = orderDetailsModel.ComboBoxProduct();
                var ProductIdListCombo = new List<SelectListItem>();

                ProductIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona un Producto" });
                foreach (var item in ProductIdCombo)
                    ProductIdListCombo.Add(new SelectListItem { Value = item.productId.ToString(), Text = item.productName });

                ViewBag.ComboProductId = ProductIdListCombo.AsEnumerable();
                return View(orderDetailsObj);
            }
        }

        // POST: Order/InsertOrderDetails
        [HttpPost]
        public ActionResult InsertOrderDetails(OrderDetailObj orderDetailsObj)
        {
            if (ModelState.IsValid)
            {
                orderDetailsModel.Register(orderDetailsObj);
                return RedirectToAction(nameof(ListOrderDetailsUser));
            }
            return View(orderDetailsObj);
        }

        // GET: Order/EditOrderDetails/5
        public ActionResult EditOrderDetails(int id)
        {
            try
            {
                var ProductIdCombo = orderDetailsModel.ComboBoxProduct();
                var ProductIdListCombo = new List<SelectListItem>();

                ProductIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona un Producto" });
                foreach (var item in ProductIdCombo)
                    ProductIdListCombo.Add(new SelectListItem { Value = item.productId.ToString(), Text = item.productName });

                ViewBag.ComboProductId = ProductIdListCombo.AsEnumerable();

                orderDetailsObj = orderDetailsModel.GetOrder(id);
                if (orderDetailsObj == null)
                {
                    return RedirectToAction(nameof(List)); ;
                }
                return View(orderDetailsObj);
            }
            catch
            {
                var ProductIdCombo = orderDetailsModel.ComboBoxProduct();
                var ProductIdListCombo = new List<SelectListItem>();

                ProductIdListCombo.Add(new SelectListItem { Value = "0", Text = "Selecciona un Producto" });
                foreach (var item in ProductIdCombo)
                    ProductIdListCombo.Add(new SelectListItem { Value = item.productId.ToString(), Text = item.productName });

                ViewBag.ComboProductId = ProductIdListCombo.AsEnumerable();

                orderDetailsObj = orderDetailsModel.GetOrder(id);

                return View(orderDetailsObj);
            }

        }

        // POST: Order/EditOrderDetails/5
        [HttpPost]
        public ActionResult EditOrderDetails(int id, OrderDetailObj orderDetailsObj)
        {
            if (ModelState.IsValid)
            {
                orderDetailsModel.EditOrderDetails(orderDetailsObj);
                return RedirectToAction(nameof(List));
            }
            return View(orderDetailsModel);
        }


        // GET: OrderController/Edit/5
        public ActionResult EditOrderDetailsUser(int id)
        {
            //OrderObj order = orderDetailsModel.GetOrderDetailsUser();
            //if (order == null)
            //{
            //    return NotFound();
            //}
            return View(/*order*/);
        }
        // POST: OrderController/Edit/5
        [HttpPost]
        public ActionResult EditOrderDetailsUser(int id, OrderObj orderObj)
        {
            if (ModelState.IsValid)
            {
                //orderModel.EditOrder(orderObj);
                return RedirectToAction(nameof(List));
            }
            return View(orderObj);
        }

        // GET: Order/DeleteOrderDetails/5
        public ActionResult DeleteOrderDetails(int id)
        {
            OrderDetailObj order = orderDetailsModel.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Order/DeleteOrderDetails/5
        [HttpPost]
        public ActionResult DeleteOrderDetails(int id, IFormCollection collection)
        {
            try
            {
                orderDetailsModel.DeleteOrderDetails(id);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

    }
}
