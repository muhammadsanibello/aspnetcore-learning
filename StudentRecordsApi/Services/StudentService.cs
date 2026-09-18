using Microsoft.EntityFrameworkCore;
using StudentRecordsApi.Data;
using StudentRecordsApi.Models;

namespace StudentRecordsApi.Services
{
    public class StudentService
    {
        private readonly StudentDbContext _context;

        public StudentService(StudentDbContext context)
        {
            _context = context;
        }

        public async Task AddStudent(Student student)
        {
            _context.Students.Add(student);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            var existingStudent = await GetStudentById(student.Id);

            if (existingStudent is null)
            {
                return false;
            }

            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Department = student.Department;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student = await GetStudentById(id);

            if (student is null)
            {
                return false;
            }

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}