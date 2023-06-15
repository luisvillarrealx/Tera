using Microsoft.AspNetCore.Mvc;
using Tera_API.Models;
using Tera_Web.Entities;

namespace Tera_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsolidatedController : Controller
    {
        private readonly IConfiguration _configuration;
        private ConsolidatedObj consolidatedObj = new ConsolidatedObj();
        ConsolidatedModel _ConsolidatedModel = new ConsolidatedModel();

        public ConsolidatedController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("GetList")]
        public ActionResult<List<ConsolidatedObj>> List()
        {
            var consolidate = _ConsolidatedModel.ListConsolidated(_configuration);
            return consolidate;
        }
        [HttpGet("GetListFilter")]
        public ActionResult<List<ConsolidatedObj>> GetConsolidatedList(DateTime min, DateTime max)
        {
            List<ConsolidatedObj> consolidatedList = _ConsolidatedModel.ListConsolidated(_configuration, min, max);

            if (consolidatedList == null || consolidatedList.Count == 0)
            {
                return NoContent(); // Devuelve respuesta 204 sin contenido si la lista está vacía
            }

            return Ok(consolidatedList); // Devuelve respuesta 200 OK con la lista consolidada
        }



    }
}
