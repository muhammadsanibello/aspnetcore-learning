using ProductCrudApi.Interfaces;
using ProductCrudApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductCrudApi.Controllers
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
            var products = _service.GetProducts();

            if (products == null)
            {
                return NoContent();
            }

            _logger.LogInformation("Products retrieved successfully.");

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct([FromRoute] Guid id)
        {
            var product = _service.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            _logger.LogInformation("Product with ID {ProductId} retrieved successfully.", id);

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            _service.AddProduct(product);

            _logger.LogInformation("Product with ID {ProductId} created successfully.", product.Id);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromRoute] Guid id, [FromBody] Product updatedProduct)
        {
            var updatedProductResult = _service.UpdateProduct(id, updatedProduct);

            if (updatedProductResult == null)
            {
                return NotFound();
            }

            _logger.LogInformation("Product with ID {ProductId} updated successfully.", id);

            return Ok(updatedProductResult);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var deleteSuccess = _service.DeleteProduct(id);

            if (!deleteSuccess)
            {
                return NotFound();
            }

            _logger.LogInformation("Product with ID {ProductId} deleted successfully.", id);

            return NoContent();
        }
    }
}