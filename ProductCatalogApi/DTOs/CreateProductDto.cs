using System.ComponentModel.DataAnnotations;

namespace ProductCatalogApi.DTOs
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
    }
}
