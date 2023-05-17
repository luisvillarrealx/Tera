using Microsoft.AspNetCore.Mvc;
using Tera_API.Entities;
using Tera_API.Models;

namespace Tera_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SiteModel _siteModel;

        public SiteController(IConfiguration configuration)
        {
            _configuration = configuration;
            _siteModel = new SiteModel();
        }

        /// <summary>
        /// Obtiene una lista de todos los sitios en la base de datos.
        /// </summary>
        [HttpGet("GetList")]
        public ActionResult<List<SiteObj>> List()
        {
            var sites = _siteModel.ListSite(_configuration);
            return sites;
        }

        /// <summary>
        /// Obtiene un sitio de la base de datos por su ID.
        /// </summary>
        [HttpGet("GetSite/{id}")]
        public ActionResult<SiteObj> Get(int id)
        {
            var site = _siteModel.GetSite(_configuration, id);
            if (site == null)
            {
                return NotFound();
            }
            return site;
        }

        /// <summary>
        /// Registra un nuevo sitio en la base de datos.
        /// </summary>
        [HttpPost("Register")]
        public IActionResult Register(SiteObj siteObj)
        {
            var result = _siteModel.Register(siteObj, _configuration);
            if (result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Edita los datos de un sitio existente en la base de datos.
        /// </summary>
        [HttpPut("EditSite")]
        public IActionResult EditSite(SiteObj siteObj)
        {
            var result = _siteModel.EditSite(siteObj, _configuration);
            if (result > 0)
            {
                return Ok(siteObj);
            }
            return BadRequest();
        }

        /// <summary>
        /// Elimina un sitio de la base de datos por su ID.
        /// </summary>
        [HttpDelete("DeleteSite")]
        public IActionResult DeleteSite(int siteId)
        {
            if (siteId == 0)
            {
                return BadRequest();
            }

            var result = _siteModel.DeleteSite(siteId, _configuration);
            if (result > 0)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
