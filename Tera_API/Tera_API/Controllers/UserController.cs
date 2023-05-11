using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Tera_API.Entities;
using Tera_API.Models;

namespace Tera_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        UserModel model = new UserModel();

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetList")]
        public List<UserObj> List()
        {
            var users = new List<UserObj>();
            UserObj u = new UserObj();
            return model.ListUser(_configuration);
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserObj user)
        {
            if (model.Register(user, _configuration) > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("EditUser")]
        public ActionResult EditUser(UserObj user)
        {

            if (model.EditUser(user, _configuration) > 0)
            {
                return Ok(user);
            }
            return BadRequest();

        }

    }
}
