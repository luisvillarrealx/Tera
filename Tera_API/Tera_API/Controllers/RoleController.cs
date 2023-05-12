using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tera_API.Entities;
using Tera_API.Models;

namespace Tera_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IConfiguration _configuration;
        RoleModel model = new RoleModel();

        public RoleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: RolesController
        [HttpGet]
        [Route("GetList")]
        public List<RoleObj> List()
        {
            var roles = new List<RoleObj>();
            RoleObj r = new RoleObj();
            return model.LisRole(_configuration);
        }

        // GET: Mostrar datos
        [HttpGet]
        [Route("GetRole/{id}")]
        public RoleObj Get(int id)
        {
            var roles = new RoleObj();
            return model.GetRole(_configuration, id);

        }

        //GET: RolesController/Create
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RoleObj rol)
        {
            try
            {
                if (model.RegisterRole(rol, _configuration) > 0)
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

        // POST: RolesController/Edit/5
        [HttpPut]
        [Route("EditRole")]
        public ActionResult EditRole(RoleObj rol)
        {
            try
            {
                if (rol == null)
                {
                    return NoContent();

                }
                else
                {
                    var persona = model.EditRole(rol, _configuration);
                    return Ok(persona);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // POST: RolesController/Delete/5
        [HttpDelete]
        [Route("DeleteRole")]
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
                    var persona = model.DeleteRole(Rol, _configuration);
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
