using Controllers.Interfaces;

namespace Controllers.Services
{
    public class ProductService : IProductService
    {
        public string GetProductName()
        {
            return "Laptop";
        }
    }
}
