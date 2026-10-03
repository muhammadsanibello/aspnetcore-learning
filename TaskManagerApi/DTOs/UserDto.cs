namespace TaskManagerApi.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<ProjectListDto> Projects { get; set; } = new List<ProjectListDto>();
    }
}