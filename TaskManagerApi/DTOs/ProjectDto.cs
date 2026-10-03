namespace TaskManagerApi.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserSummaryDto Owner { get; set; }

        public ICollection<TaskSummaryDto> Tasks { get; set; } = new List<TaskSummaryDto>();
    }
}
