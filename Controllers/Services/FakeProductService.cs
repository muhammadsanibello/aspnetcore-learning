using Controllers.Interfaces;

namespace Controllers.Services
{
    public class FakeProductService : IProductService
    {
        public string GetProductName()
        {
            return "Fake Laptop";
        }
    }
}
