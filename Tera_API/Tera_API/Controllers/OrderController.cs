using Microsoft.AspNetCore.Mvc;
using Tera_API.Entities;
using Tera_API.Models;

namespace Tera_API.Controllers
{
    /// <summary>
    /// Controlador para la gestión de pedidos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IConfiguration _configuration;
        OrderModel orderModel = new OrderModel();

        /// <summary>
        /// Crea una nueva instancia del controlador OrderController.
        /// </summary>
        /// <param name="configuration">La configuración de la aplicación.</param>
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Obtiene la lista de pedidos.
        /// </summary>
        /// <returns>Una lista de objetos OrderObj que representan los pedidos.</returns>
        [HttpGet]
        [Route("GetList")]
        public List<OrderObj> List()
        {
            var roles = new List<OrderObj>();
            OrderObj orderObj = new OrderObj();
            return orderModel.ListOrder(_configuration);
        }

        /// <summary>
        /// Obtiene un pedido por su ID.
        /// </summary>
        /// <param name="id">El ID del pedido.</param>
        /// <returns>Un objeto OrderObj que representa el pedido encontrado.</returns>
        [HttpGet]
        [Route("GetOrder/{id}")]
        public OrderObj Get(int id)
        {
            var roles = new OrderObj();
            return orderModel.GetOrder(_configuration, id);
        }

        /// <summary>
        /// Registra un nuevo pedido.
        /// </summary>
        /// <param name="orderObj">El objeto OrderObj que contiene los datos del pedido a registrar.</param>
        /// <returns>Un ActionResult que indica el resultado de la operación.</returns>
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(List<OrderObj> orders)
        {
            try
            {
                if (orderModel.Register(orders, _configuration) > 0)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                //bitacoraM.RegistrarErrores(usuario.Usuario, ex, ControllerContext.ActionDescriptor.ActionName, _configuration);
                return BadRequest("Se presentó un inconveniente");
            }
        }

        /// <summary>
        /// Actualiza los datos de un pedido existente.
        /// </summary>
        /// <param name="orderObj">El objeto OrderObj que contiene los datos actualizados del pedido.</param>
        /// <returns>Un ActionResult que contiene el objeto OrderObj actualizado o un error si la operación falla.</returns>
        [HttpPut]
        [Route("EditOrder")]
        public ActionResult EditOrder(OrderObj orderObj)
        {
            try
            {
                if (orderObj == null)
                {
                    return NoContent();
                }
                else
                {
                    var persona = orderModel.EditOrder(orderObj, _configuration);
                    return Ok(persona);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Elimina un pedido por su ID.
        /// </summary>
        /// <param name="Rol">El ID del pedido a eliminar.</param>
        /// <returns>Un ActionResult que indica el resultado de la operación.</returns>
        [HttpDelete]
        [Route("DeleteOrder")]
        public ActionResult Delete(int Rol)
        {
            try
            {
                if (Rol == 0)
                {
                    NotFound();
                }
                else
                {
                    var persona = orderModel.DeleteOrder(Rol, _configuration);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
