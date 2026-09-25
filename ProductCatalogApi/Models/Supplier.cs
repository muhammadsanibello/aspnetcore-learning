using System.Text.Json.Serialization;

namespace ProductCatalogApi.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Product> Suppliers { get; set; } = new List<Product>();
    }
}
