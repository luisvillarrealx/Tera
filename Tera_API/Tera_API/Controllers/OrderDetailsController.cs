using Microsoft.AspNetCore.Mvc;
using Tera_API.Entities;
using Tera_API.Models;

namespace Tera_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : Controller
    {
        private readonly IConfiguration _configuration;
        OrderDetailsModel OrderDetailsModel = new OrderDetailsModel();

        public OrderDetailsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: RolesController
        [HttpGet]
        [Route("GetList")]
        public List<OrderDetailsObj> List()
        {
            var roles = new List<OrderDetailsObj>();
            OrderDetailsObj orderDetailsObj = new OrderDetailsObj();
            return OrderDetailsModel.ListOrderOrderDetails(_configuration);
        }
        /*
        *  El método List() realiza una solicitud HTTP GET para obtener una lista de detalles de pedidos.
        *  La ruta del controlador se establece en "api/[controller]", donde [controller] se reemplaza por el nombre del controlador.
        *  Se utiliza el atributo HttpGet para especificar que este método responde a solicitudes GET.
        *  El atributo Route se utiliza para establecer una ruta adicional para el método.
        *  El método crea una nueva instancia de OrderDetailsObj y devuelve la lista de detalles de pedidos utilizando el modelo OrderDetailsModel.
        */

        // GET: Mostrar datos
        [HttpGet]
        [Route("GetOrderDetails/{id}")]
        public OrderDetailsObj Get(int id)
        {
            var roles = new OrderDetailsObj();
            return OrderDetailsModel.GetOrderDetails(_configuration, id);
        }
        /*
        *  El método Get() realiza una solicitud HTTP GET para obtener un detalle de pedido específico por su ID.
        *  La ruta del controlador se establece en "api/[controller]/GetRole/{id}", donde [controller] se reemplaza por el nombre del controlador y {id} es un parámetro de ruta.
        *  Se utiliza el atributo HttpGet para especificar que este método responde a solicitudes GET.
        *  El atributo Route se utiliza para establecer una ruta adicional para el método.
        *  El método devuelve el detalle de pedido utilizando el modelo OrderDetailsModel y pasando el ID y la configuración como argumentos.
        */

        //GET: RolesController/Create
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(OrderDetailsObj orderDetailsObj)
        {
            try
            {
                if (OrderDetailsModel.Register(orderDetailsObj, _configuration) > 0)
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
        /*
        *  El método Register() realiza una solicitud HTTP POST para registrar un nuevo detalle de pedido.
        *  La ruta del controlador se establece en "api/[controller]/Register", donde [controller] se reemplaza por el nombre del controlador.
        *  Se utiliza el atributo HttpPost para especificar que este método responde a solicitudes POST.
        *  El atributo Route se utiliza para establecer una ruta adicional para el método.
        *  El método intenta registrar el detalle de pedido utilizando el modelo OrderDetailsModel y devuelve un resultado Ok o BadRequest según el resultado.
        */

        // POST: RolesController/Edit/5
        [HttpPut]
        [Route("EditOrderDetails")]
        public ActionResult EditOrderDetails(OrderDetailsObj orderDetailsObj)
        {
            try
            {
                if (orderDetailsObj == null)
                {
                    return NoContent();
                }
                else
                {
                    var persona = OrderDetailsModel.EditOrderDetails(orderDetailsObj, _configuration);
                    return Ok(persona);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /*
        * El método EditOrder() realiza una solicitud HTTP PUT para editar un detalle de pedido existente.
        * La ruta del controlador se establece en "api/[controller]/EditRole", donde [controller] se reemplaza por el nombre del controlador.
        * Se utiliza el atributo HttpPut para especificar que este método responde a solicitudes PUT.
        * El atributo Route se utiliza para establecer una ruta adicional para el método.
        * El método intenta editar el detalle de pedido utilizando el modelo OrderDetailsModel y devuelve un resultado Ok o StatusCode según el resultado.
        */

        // POST: RolesController/Delete/5
        [HttpDelete]
        [Route("DeleteOrderDetails")]
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
                    var persona = OrderDetailsModel.DeleteOrderDetails(Rol, _configuration);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /*
        *  El método Delete() realiza una solicitud HTTP DELETE para eliminar un detalle de pedido por su ID.
        *  La ruta del controlador se establece en "api/[controller]/DeleteRole", donde [controller] se reemplaza por el nombre del controlador.
        *  Se utiliza el atributo HttpDelete para especificar que este método responde a solicitudes DELETE.
        *  El atributo Route se utiliza para establecer una ruta adicional para el método.
        *  El método intenta eliminar el detalle de pedido utilizando el modelo OrderDetailsModel y devuelve un resultado Ok o StatusCode según el resultado.
        */
    }
}
