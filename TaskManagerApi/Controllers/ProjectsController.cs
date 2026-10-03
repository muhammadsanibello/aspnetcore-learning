using TaskManagerApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Interfaces;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectDto dto)
        {
            var createdProject = await _projectService.CreateProjectAsync(dto);

            return CreatedAtAction(nameof(GetProject), new { id  = createdProject.Id }, createdProject);
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetProjectsAsync();

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject([FromRoute] int id)
        {
            var project = await _projectService.GetProjectAsync(id);

            if (project is null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject([FromRoute] int id, [FromBody] UpdateProjectDto dto)
        {
            var updateSuccess = await _projectService.UpdateProjectAsync(id, dto);

            if (!updateSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            var deletionSuccess = await _projectService.DeleteProjectAsync(id);

            if (!deletionSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}