using StudentRecordsApi.Services;
using Microsoft.AspNetCore.Mvc;
using StudentRecordsApi.Models;

namespace StudentRecordsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            await _studentService.AddStudent(student);

            return Ok(student);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetStudents();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var student = await _studentService.GetStudentById(id);

            if (student is null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] int id, [FromBody] Student student)
        {
            student.Id = id;

            var updated = await _studentService.UpdateStudent(student);

            if (!updated)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var deleted = await _studentService.DeleteStudent(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}