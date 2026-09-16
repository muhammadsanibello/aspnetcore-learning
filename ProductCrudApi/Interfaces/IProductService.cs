using ProductCrudApi.Models;

namespace ProductCrudApi.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<Product>? GetProducts();

        public void AddProduct(Product product);

        public Product? GetProductById(Guid id);

        public Product? UpdateProduct(Guid id, Product updatedProduct);

        public bool DeleteProduct(Guid id);
    }
}
