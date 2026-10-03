using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.DTOs
{
    public class CreateUserDto
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
