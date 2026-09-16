using System.ComponentModel.DataAnnotations;

namespace ProductCrudApi.Models
{
    public class Product
    {
        public Guid Id { get; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range (0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
