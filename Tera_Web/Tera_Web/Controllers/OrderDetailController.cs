using Microsoft.AspNetCore.Mvc;
using Tera_Web.Entities;
using Tera_Web.Models;
using Tera_Web.Filters;

namespace Tera_Web.Controllers
{
    [FilterSession]
    public class OrderDetailController : Controller
    {
        OrderDetailObj orderDetailsObj = new OrderDetailObj();
        OrderDetailModel orderDetailsModel = new OrderDetailModel();

        // GET: Order/List
        public IActionResult List()
        {
            List<OrderDetailObj> orders = orderDetailsModel.GetOrderDetailsList();
            return View(orders);
        }

        // GET: Order/InsertOrderDetails
        public ActionResult InsertOrderDetails()
        {
            return View();
        }

        // POST: Order/InsertOrderDetails
        [HttpPost]
        public ActionResult InsertOrderDetails(OrderDetailObj orderDetailsObj)
        {
            if (ModelState.IsValid)
            {
                orderDetailsModel.Register(orderDetailsObj);
                return RedirectToAction(nameof(List));
            }
            return View(orderDetailsObj);
        }

        // GET: Order/EditOrderDetails/5
        public ActionResult EditOrderDetails(int id)
        {
            OrderDetailObj order = orderDetailsModel.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
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
