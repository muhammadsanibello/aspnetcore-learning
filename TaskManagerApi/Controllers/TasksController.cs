using TaskManagerApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Interfaces;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("/api/projects/{projectId}/tasks")]
        public async Task<IActionResult> CreateTask([FromRoute] int projectId, CreateTaskDto dto)
        {
            var createdTask = await _taskService.CreateTaskAsync(projectId, dto);

            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpGet("/api/projects/{projectId}/tasks")]
        public async Task<IActionResult> GetProjectTasks([FromRoute] int projectId, [FromQuery] string? status, [FromQuery] string? priority, [FromQuery] int? page, [FromQuery] int? pageSize)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest();
            }

            if (status is null && priority is null && page is null && pageSize is null)
            {
                var projectTasks = await _taskService.GetProjectTasksAsync(projectId);

                return Ok(projectTasks);
            }
            else if (status is null && priority is null && (page is not null && pageSize is not null))
            {
                var tasksByPageSize = await _taskService.GetProjectTasksByPageSizeAsync(projectId, (int)page, (int)pageSize);

                return Ok(tasksByPageSize);
            }
            else if (priority is null && (page is null && pageSize is null))
            {
                var tasksByStatus = await _taskService.GetProjectTasksByStatusAsync(projectId, status);

                return Ok(tasksByStatus);
            }
            else if (status is null && (page is null && pageSize is null))
            {
                var tasksByPriority = await _taskService.GetProjectTasksByPriorityAsync(projectId, priority);

                return Ok(tasksByPriority);
            }
            else if (status is not null && priority is not null && (page is null && pageSize is null))
            {
                var tasksByStatusAndByPriority = await _taskService.GetProjectTasksByStatusAndPriorityAsync(projectId, status, priority);

                return Ok(tasksByStatusAndByPriority);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask([FromRoute] int id)
        {
            var task = await _taskService.GetTaskAsync(id);

            if (task is null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask([FromRoute] int id, [FromBody] UpdateTaskDto dto)
        {
            var updateSuccess = await _taskService.UpdateTaskAsync(id, dto);

            if (!updateSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            var deletionSuccess = await _taskService.DeleteTaskAsync(id);

            if (!deletionSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}