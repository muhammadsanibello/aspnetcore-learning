using Microsoft.AspNetCore.Mvc;
using ProductCatalogApi.DTOs;
using ProductCatalogApi.Interfaces;

namespace ProductCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            var products = await _service.GetProductsAsync(page, pageSize);

            //i   return NotFound();f (!product)
            //{
            // 
            //}

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            var product = await _service.CreateProductAsync(dto);

            return Ok(product);
        }
    }
}
