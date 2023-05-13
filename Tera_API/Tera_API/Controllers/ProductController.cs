using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tera_API.Entities;
using Tera_API.Models;

namespace Tera_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        ProductModel productModel = new ProductModel();

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/<ProductosController>

        [HttpGet]
        [Route("GetList")]
        public List<ProductObj> List()
        {
            var Productos = new List<ProductObj>();
            ProductObj p = new ProductObj();
            return productModel.ListProduct(_configuration);

        }
        // GET: Mostrar datos

        [HttpGet]
        [Route("GetProducto/{id}")]
        public ProductObj Get(int id)
        {
            var Productos = new ProductObj();
            return productModel.GetProduct(_configuration, id);

        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(ProductObj productObj)
        {
            if (productModel.Register(productObj, _configuration) > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("EditProduct")]
        public ActionResult EditProduct(ProductObj productObj)
        {
            try
            {
                if (productObj == null)
                {
                    return NoContent();

                }
                else
                {
                    var persona = productModel.EditProduct(productObj, _configuration);
                    return Ok(productObj);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public ActionResult DeleteProduct(int IdProducto)
        {
            try
            {
                if (IdProducto == null)
                {
                    NotFound();
                }
                else
                {
                    var producto = productModel.DeleteProduct(IdProducto, _configuration);
                }
                return Ok(IdProducto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
