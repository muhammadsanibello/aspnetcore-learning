using System.ComponentModel.DataAnnotations;

namespace Controllers.Models
{
    public class Product
    {
        [Required]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
