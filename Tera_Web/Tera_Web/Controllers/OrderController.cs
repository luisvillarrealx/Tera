using Microsoft.AspNetCore.Mvc;
using Tera_Web.Entities;
using Tera_Web.Models;
using Tera_Web.Filters;

namespace Tera_Web.Controllers
{
    [FilterSession]
    public class OrderController : Controller
    {
        OrderObj orderObj = new OrderObj();
        OrderModel orderModel = new OrderModel();

        // GET: OrderController
        public ActionResult List()
        {
            List<OrderObj> orders = orderModel.GetOrderList();
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
                orderModel.Register(orderObj);
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

            List<OrderObj> _Productos = new List<OrderObj>();
            _Productos = orderModel.GetProductList().ToList();
            return View(_Productos);
        }
        [HttpPost]
        public ActionResult CreateOrder(OrderObj orderObj)
        {

            if (ModelState.IsValid)
            {
                orderModel.Register(orderObj);
                return RedirectToAction(nameof(List));
            }
            return View();
        }

    }
}
