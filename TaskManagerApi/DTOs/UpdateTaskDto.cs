namespace TaskManagerApi.DTOs
{
    public class UpdateTaskDto
    {
        public string? Title { get; set; }
        public DateOnly? DueDate { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
    }
}
