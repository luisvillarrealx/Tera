using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using Tera_Web.Entities;
using Tera_Web.Filters;
using Tera_Web.Models;

namespace Tera_Web.Controllers
{
    [FilterSession]
    public class OrderController : Controller
    {
        //Objs
        OrderObj orderObj = new OrderObj();
        //Models
        OrderModel orderModel = new OrderModel();

        List<OrderObj> OrderOBJList = new List<OrderObj>();

        // GET: OrderController
        [FilterSessionValidation]
        public ActionResult List()
        {

            List<OrderObj> orders = orderModel.GetOrderList();
            return View(orders);
        }
        public ActionResult ListUser()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            List<OrderObj> orders = orderModel.GetOrderListUser(id);
            return View(orders);
        }
        // GET: OrderController/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        public ActionResult Register(OrderObj orderObj)
        {
            if (ModelState.IsValid)
            {
                //orderModel.Register(orderObj);
                return RedirectToAction(nameof(List));
            }
            return View(orderObj);
        }
        // GET: OrderController/Edit/5
        public ActionResult EditOrder(int id)
        {
            OrderObj order = orderModel.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        // POST: OrderController/Edit/5
        [HttpPost]
        public ActionResult EditOrder(int id, OrderObj orderObj)
        {
            if (ModelState.IsValid)
            {
                orderModel.EditOrder(orderObj);
                return RedirectToAction(nameof(List));
            }
            return View(orderObj);
        }

        // GET: OrderController/Delete/5
        public ActionResult DeleteOrder(int id)
        {
            OrderObj order = orderModel.GetOrder(id);
            return RedirectToAction(nameof(List));
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        public ActionResult DeleteOrder(int id, IFormCollection collection)
        {
            orderModel.DeleteOrder(id);
            return RedirectToAction(nameof(List));
        }

        public ActionResult CreateOrder()
        {
            HttpContext.Session.Remove("Orders");

            List<OrderObj> _Productos = new List<OrderObj>();
            _Productos = orderModel.GetProductList().ToList();
            return View(_Productos);
        }
        [HttpPost]
        public IActionResult CreateOrder(List<OrderObj> orders)
        {
            try
            {
                OrderOBJList = JsonConvert.DeserializeObject<List<OrderObj>>(HttpContext.Session.GetString("Orders"));
                //int total = Convert.ToInt32(HttpContext.Session.GetString("Total"));
                orderModel.Register(OrderOBJList);

                return RedirectToAction(nameof(ListUser));
            }
            catch
            {
                return RedirectToAction(nameof(ListUser));
            }



        }
        [HttpPost]
        public JsonResult AddProduct(int id, int quantity, int cost)
        {

            if (HttpContext.Session.GetString("Orders") != null)
            {
                OrderOBJList = JsonConvert.DeserializeObject<List<OrderObj>>(HttpContext.Session.GetString("Orders"));
                OrderOBJList.Add(new OrderObj
                {
                    userId = Convert.ToInt32(HttpContext.Session.GetString("userId")),
                    productId = id,
                    orderDetailsQuantity = quantity,
                    productCost = cost * quantity
                });
                HttpContext.Session.SetString("Orders", JsonConvert.SerializeObject(OrderOBJList));
            }
            else
            {
                OrderOBJList.Add(new OrderObj
                {
                    userId = Convert.ToInt32(HttpContext.Session.GetString("userId")),
                    productId = id,
                    orderDetailsQuantity = quantity,
                    productCost = cost * quantity
                });
                HttpContext.Session.SetString("Orders", JsonConvert.SerializeObject(OrderOBJList));
            }

            int total = OrderOBJList.Sum(x => x.productCost);
            HttpContext.Session.SetInt32("Total", total);
            return Json(new { result = true });
        }

        public JsonResult DeleteProduct(int id)
        {

            if (HttpContext.Session.GetString("Orders") != null)
            {
                OrderOBJList = JsonConvert.DeserializeObject<List<OrderObj>>(HttpContext.Session.GetString("Orders"));
                OrderOBJList.RemoveAll(x => x.productId == id);

                HttpContext.Session.SetString("Orders", JsonConvert.SerializeObject(OrderOBJList));
            }
            int total = OrderOBJList.Sum(x => x.productCost);
            HttpContext.Session.SetInt32("Total", total);
            return Json(new { result = true });
        }
    }
}
