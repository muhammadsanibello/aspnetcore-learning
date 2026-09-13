using Controllers.Interfaces;
using Controllers.Models;
using Controllers.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService service, ILogger<ProductsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok("Products endpoint is working!");
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct([FromRoute(Name = "id")] int productId)
        {
            if (productId <= 0)
            {
                _logger.LogWarning("Invalid product ID received: {ProductId}", productId);

                return NotFound();
            }

            _logger.LogInformation("Product {ProductId} was requested.", productId);

            return Ok($"Product ID: {productId}");
        }

        [HttpGet("search")]
        public IActionResult SearchProducts(string category)
        {
            return Ok($"Searching for: {category}");
        }

        [HttpGet("search1")]
        public IActionResult SearchProducts([FromQuery] string category, [FromQuery] int page)
        {
            return Ok($"Category: {category}, Page: {page}");
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            return CreatedAtAction(nameof(GetProduct), new { id = 25 }, product);
        }

        [HttpGet("header-test")]
        public IActionResult HeaderTest([FromHeader] string UserAgent)
        {
            return Ok($"User-Agent: {UserAgent}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            return NoContent();
        }

        [HttpGet("service-test")]
        public IActionResult GetProductName()
        {
            string productName = _service.GetProductName();

            return Ok(productName);
        }

        [HttpGet("logging-test")]
        public IActionResult LoggingTest()
        {
            _logger.LogInformation("Logging test endpoint was called.");

            return Ok("Logging works!");
        }

        [HttpGet("error-test")]
        public IActionResult ErrorTest()
        {
            try
            {
                throw new Exception("Something went wrong while processing the product.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while processing the product.");

                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}