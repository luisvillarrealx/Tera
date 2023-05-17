using Microsoft.AspNetCore.Mvc;
using Tera_API.Entities;
using Tera_API.Models;

namespace Tera_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ProductModel _productModel;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            _productModel = new ProductModel();
        }

        /// <summary>
        /// Obtiene una lista de todos los productos en la base de datos.
        /// </summary>
        [HttpGet("GetList")]
        public ActionResult<List<ProductObj>> List()
        {
            var products = _productModel.ListProduct(_configuration);
            return products;
        }

        /// <summary>
        /// Obtiene un producto de la base de datos por su ID.
        /// </summary>
        [HttpGet("GetProducto/{id}")]
        public ActionResult<ProductObj> Get(int id)
        {
            var product = _productModel.GetProduct(_configuration, id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        /// <summary>
        /// Registra un nuevo producto en la base de datos.
        /// </summary>
        [HttpPost("Register")]
        public IActionResult Register(ProductObj productObj)
        {
            try
            {
                var result = _productModel.Register(productObj, _configuration);
                if (result > 0)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Edita los datos de un producto existente en la base de datos.
        /// </summary>
        [HttpPut("EditProduct")]
        public IActionResult EditProduct(ProductObj productObj)
        {
            try
            {
                if (productObj == null)
                {
                    return NoContent();
                }

                var result = _productModel.EditProduct(productObj, _configuration);
                if (result > 0)
                {
                    return Ok(productObj);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Elimina un producto de la base de datos por su ID.
        /// </summary>
        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int IdProducto)
        {
            try
            {
                if (IdProducto == 0)
                {
                    return NotFound();
                }

                var result = _productModel.DeleteProduct(IdProducto, _configuration);
                if (result > 0)
                {
                    return Ok(IdProducto);
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
