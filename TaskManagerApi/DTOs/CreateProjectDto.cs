using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.DTOs
{
    public class CreateProjectDto
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
    }
}
