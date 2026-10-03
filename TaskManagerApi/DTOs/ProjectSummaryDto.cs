namespace TaskManagerApi.DTOs
{
    public class ProjectSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserSummaryDto Owner { get; set; }
    }
}
