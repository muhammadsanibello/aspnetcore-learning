using ProductCrudApi.Interfaces;
using ProductCrudApi.Models;

namespace ProductCrudApi.Services
{
    public class ProductService : IProductService
    {
        private List<Product> _products = new();

        public IEnumerable<Product>? GetProducts()
        {
            if (!_products.Any())
            {
                return null;
            }

            return _products;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }
        public Product? GetProductById(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Product? UpdateProduct(Guid id, Product updatedProduct)
        {
            Product? product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Quantity = updatedProduct.Quantity;

            // Updated successfully
            return product;
        }

        public bool DeleteProduct(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            return _products.Remove(product);
        }
    }
}