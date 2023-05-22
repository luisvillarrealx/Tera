using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Tera_API.Entities;
using Tera_API.Models;

namespace Tera_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        AuthModel authModel = new AuthModel();
        //RolesModel rolmodel = new RolesModel();

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("UserValidate")]
        public ActionResult<UserObj> ValidarUsuario(UserObj userObj)
        {
            try
            {
                var datos = authModel.UserValidate(userObj, _configuration);
                if (datos != null)
                    return Ok(datos);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                //bitacoraM.RegistrarErrores(usuario.Usuario, ex, ControllerContext.ActionDescriptor.ActionName, _configuration);
                return BadRequest("Se presentó un inconveniente");
            }
        }

    }
}
