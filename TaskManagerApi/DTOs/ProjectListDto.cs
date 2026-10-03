namespace TaskManagerApi.DTOs
{
    public class ProjectListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<TaskSummaryDto> Tasks { get; set; } = new List<TaskSummaryDto>();
    }
}