using System.Text.Json.Serialization;

namespace ProductCatalogApi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public int? CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
