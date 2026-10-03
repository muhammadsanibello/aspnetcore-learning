namespace TaskManagerApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
