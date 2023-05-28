using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tera_Web.Entities;
using Tera_Web.Filters;
using Tera_Web.Models;

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
            HttpContext.Session.Remove("Orders");

            List<OrderObj> _Productos = new List<OrderObj>();
            _Productos = orderModel.GetProductList().ToList();
            return View(_Productos);
        }
        [HttpPost]
        public IActionResult CreateOrder(string[] productId, List<OrderObj> orders)
        {
            // Aquí puedes usar el array productId y la lista de órdenes para procesar los datos
            // Recuerda que la longitud de productId y orders debería ser la misma

            // Ejemplo de uso
            for (int i = 0; i < productId.Length; i++)
            {
                string currentProductId = productId[i];
                OrderObj currentOrder = orders[i];

                // Procesar los datos según tus necesidades
            }

            return RedirectToAction(nameof(List));
            // Resto de la lógica de la acción CreateOrder
        }
        [HttpPost]
        public JsonResult AddProduct(int id, int quantity, int cost)
        {

            List<OrderObj> OrderOBJList = new List<OrderObj>();

            if (HttpContext.Session.GetString("Orders") != null)
            {
                OrderOBJList = JsonConvert.DeserializeObject<List<OrderObj>>(HttpContext.Session.GetString("Orders"));
                OrderOBJList.Add(new OrderObj
                {
                    userId = Convert.ToInt32(HttpContext.Session.GetString("userId")),
                    productId = id,
                    cuantity = quantity,
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
                    cuantity = quantity,
                    productCost = cost * quantity
                });
                HttpContext.Session.SetString("Orders", JsonConvert.SerializeObject(OrderOBJList));
            }

            int total = OrderOBJList.Sum(x => x.productCost);
            return Json(new { result = true });
        }

        public JsonResult DeleteProduct(int id)
        {

            List<OrderObj> OrderOBJList = new List<OrderObj>();

            if (HttpContext.Session.GetString("Orders") != null)
            {
                OrderOBJList = JsonConvert.DeserializeObject<List<OrderObj>>(HttpContext.Session.GetString("Orders"));
                OrderOBJList.RemoveAll(x => x.productId == id);

                HttpContext.Session.SetString("Orders", JsonConvert.SerializeObject(OrderOBJList));
            }
            int total = OrderOBJList.Sum(x => x.productCost);

            return Json(new { result = true });
        }
    }
}
