using Microsoft.AspNetCore.Mvc;
using Tera_API.Entities;
using Tera_API.Models;

namespace Tera_API.Controllers
{
    /// <summary>
    /// Controlador para la gestión de usuarios.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private UserModel userModel = new UserModel();

        /// <summary>
        /// Crea una nueva instancia del controlador UserController.
        /// </summary>
        /// <param name="configuration">La configuración de la aplicación.</param>
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Obtiene la lista de usuarios.
        /// </summary>
        /// <returns>Una lista de objetos UserObj que representan a los usuarios.</returns>
        [HttpGet]
        [Route("GetList")]
        public List<UserObj> List()
        {
            return userModel.ListUser(_configuration);
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario.</param>
        /// <returns>Un objeto UserObj que representa al usuario encontrado.</returns>
        [HttpGet]
        [Route("GetUser/{id}")]
        public UserObj Get(int id)
        {
            return userModel.GetUser(_configuration, id);
        }

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="userObj">El objeto UserObj que contiene los datos del usuario a registrar.</param>
        /// <returns>Un IActionResult que indica el resultado de la operación.</returns>
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserObj userObj)
        {
            if (userModel.Register(userObj, _configuration) > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("EmailExists")]
        public string EmailExists(string validateEmailExists)
        {
            return userModel.EmailExists(validateEmailExists);
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="userObj">El objeto UserObj que contiene los datos actualizados del usuario.</param>
        /// <returns>Un ActionResult que contiene el objeto UserObj actualizado o un error si la operación falla.</returns>
        [HttpPut]
        [Route("EditUser")]
        public ActionResult EditUser(UserObj userObj)
        {
            if (userModel.EditUser(userObj, _configuration) > 0)
            {
                return Ok(userObj);
            }
            return BadRequest();
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="userId">El ID del usuario a eliminar.</param>
        /// <returns>Un ActionResult que indica el resultado de la operación.</returns>
        [HttpDelete]
        [Route("DeleteUser")]
        public ActionResult DeleteUser(int userId)
        {
            try
            {
                if (userId == null)
                {
                    NotFound();
                }
                else
                {
                    var persona = userModel.DeleteUser(userId, _configuration);
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