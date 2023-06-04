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

    }
}
