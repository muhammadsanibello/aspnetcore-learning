namespace TaskManagerApi.Models
{
    public class TaskPriority
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
