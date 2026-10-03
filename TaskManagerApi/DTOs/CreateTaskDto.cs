using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.DTOs
{
    public class CreateTaskDto
    {
        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [Required]
        public DateOnly DueDate { get; set; }

        [Range(1, int.MaxValue)]
        public int StatusId { get; set; }

        [Range(1, int.MaxValue)]
        public int PriorityId { get; set; }
    }
}