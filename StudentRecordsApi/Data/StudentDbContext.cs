using Microsoft.EntityFrameworkCore;
using StudentRecordsApi.Models;

namespace StudentRecordsApi.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}