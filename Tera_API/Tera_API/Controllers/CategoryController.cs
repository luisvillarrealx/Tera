using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tera_API.Entities;
using Tera_API.Models;

namespace Tera_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly CategoryModel _categoryModel;

        public CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;
            _categoryModel = new CategoryModel();
        }

        /// <summary>
        /// Obtiene una lista de todos los roles en la base de datos.
        /// </summary>
        [HttpGet("GetList")]
        public ActionResult<List<CategoryObj>> List()
        {
            var roles = _categoryModel.ListCategory(_configuration);
            return roles;
        }

        /// <summary>
        /// Obtiene un rol de la base de datos por su ID.
        /// </summary>
        [HttpGet("GetRole/{id}")]
        public ActionResult<CategoryObj> Get(int id)
        {
            var role = _categoryModel.GetCategory(_configuration, id);
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
        public IActionResult Register(CategoryObj CategoryObj)
        {
            try
            {
                var result = _categoryModel.RegisterCategory(CategoryObj, _configuration);
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
        public IActionResult EditRole(CategoryObj CategoryObj)
        {
            try
            {
                if (CategoryObj == null)
                {
                    return NoContent();
                }

                var result = _categoryModel.EditCategory(CategoryObj, _configuration);
                if (result > 0)
                {
                    return Ok(CategoryObj);
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

                var result = _categoryModel.DeleteCategory(roleId, _configuration);
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
