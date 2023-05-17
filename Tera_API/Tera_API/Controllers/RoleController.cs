using Microsoft.AspNetCore.Mvc;
using Tera_API.Entities;
using Tera_API.Models;

namespace Tera_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly RoleModel _roleModel;

        public RoleController(IConfiguration configuration)
        {
            _configuration = configuration;
            _roleModel = new RoleModel();
        }

        /// <summary>
        /// Obtiene una lista de todos los roles en la base de datos.
        /// </summary>
        [HttpGet("GetList")]
        public ActionResult<List<RoleObj>> List()
        {
            var roles = _roleModel.LisRole(_configuration);
            return roles;
        }

        /// <summary>
        /// Obtiene un rol de la base de datos por su ID.
        /// </summary>
        [HttpGet("GetRole/{id}")]
        public ActionResult<RoleObj> Get(int id)
        {
            var role = _roleModel.GetRole(_configuration, id);
            if (role == null)
            {
                return NotFound();
            }
            return role;
        }

        /// <summary>
        /// Registra un nuevo rol en la base de datos.
        /// </summary>
        [HttpPost("Register")]
        public IActionResult Register(RoleObj roleObj)
        {
            try
            {
                var result = _roleModel.RegisterRole(roleObj, _configuration);
                if (result > 0)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                //bitacoraM.RegistrarErrores(usuario.Usuario, ex, ControllerContext.ActionDescriptor.ActionName, _configuration);
                return BadRequest("Se presentó un inconveniente");
            }
        }

        /// <summary>
        /// Edita los datos de un rol existente en la base de datos.
        /// </summary>
        [HttpPut("EditRole")]
        public IActionResult EditRole(RoleObj roleObj)
        {
            try
            {
                if (roleObj == null)
                {
                    return NoContent();
                }

                var result = _roleModel.EditRole(roleObj, _configuration);
                if (result > 0)
                {
                    return Ok(roleObj);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Elimina un rol de la base de datos por su ID.
        /// </summary>
        [HttpDelete("DeleteRole")]
        public IActionResult Delete(int roleId)
        {
            try
            {
                if (roleId == 0)
                {
                    return NotFound();
                }

                var result = _roleModel.DeleteRole(roleId, _configuration);
                if (result > 0)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
