namespace TaskManagerApi.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public int StatusId { get; set; }
        public TaskStatus? Status { get; set; }

        public int PriorityId { get; set; }
        public TaskPriority? Priority { get; set; }
    }
}