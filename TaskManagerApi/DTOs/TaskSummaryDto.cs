namespace TaskManagerApi.DTOs
{
    public class TaskSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public TaskStatusSummaryDto Status { get; set; }
        public TaskPrioritySummaryDto Priority { get; set; }
    }
}