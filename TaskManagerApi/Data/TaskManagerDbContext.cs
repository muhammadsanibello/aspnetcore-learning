using TaskManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi.Data
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Models.TaskStatus> TaskStatuses { get; set; }
        public DbSet<TaskPriority> TaskPriorities { get; set; }
    }
}
