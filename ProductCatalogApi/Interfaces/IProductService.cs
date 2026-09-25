using ProductCatalogApi.DTOs;
using ProductCatalogApi.Models;

namespace ProductCatalogApi.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsAsync(int page, int pageSize);
        Task<List<Category>> GetCategoriesAsync();
        Task<ProductDto> CreateProductAsync(CreateProductDto dto);
    }
}
