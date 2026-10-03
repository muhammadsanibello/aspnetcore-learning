namespace TaskManagerApi.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public ProjectSummaryDto Project { get; set; }
        public TaskStatusSummaryDto Status { get; set; }
        public TaskPrioritySummaryDto Priority { get; set; }
    }
}