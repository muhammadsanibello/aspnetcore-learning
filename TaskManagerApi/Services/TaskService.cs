using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using TaskManagerApi.Interfaces;

namespace TaskManagerApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskManagerDbContext _context;
        private readonly ILogger<TaskService> _logger;

        public TaskService(TaskManagerDbContext context, ILogger<TaskService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TaskDto> CreateTaskAsync(int projectId, CreateTaskDto dto)
        {
            var task = new Models.Task
            {
                Title = dto.Title,
                DueDate = dto.DueDate,
                ProjectId = projectId,
                StatusId = dto.StatusId,
                PriorityId = dto.PriorityId,
            };

            _context.Tasks.Add(task);

            // Commit changes to a database
            await _context.SaveChangesAsync();

            _logger.LogInformation("Task {id} created", task.Id);

            await _context.Tasks
                .Entry(task)
                .Reference(t => t.Project)
                .LoadAsync();

            await _context.Projects
                .Entry(task.Project)
                .Reference(p => p.User)
                .LoadAsync();

            await _context.Tasks
                .Entry(task)
                .Reference(t => t.Status)
                .LoadAsync();

            await _context.Tasks
                .Entry(task)
                .Reference(t => t.Priority)
                .LoadAsync();

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                Project = new ProjectSummaryDto
                {
                    Id = task.Project.Id,
                    Name = task.Project.Name,
                    CreatedAt = task.Project.CreatedAt,
                    Owner = new UserSummaryDto
                    {
                        Id = task.Project.User.Id,
                        Name = task.Project.User.Name,
                        Email = task.Project.User.Email
                    }
                },
                Status = new TaskStatusSummaryDto
                {
                    Name = task.Status.Name,
                },
                Priority = new TaskPrioritySummaryDto
                {
                    Name = task.Priority.Name
                }
            };
        }

        public async Task<List<TaskDto>> GetProjectTasksAsync(int projectId)
        {
            var tasksDto = new List<TaskDto>();

            var projectTasks = await _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .Include(t => t.Project)
                .ThenInclude(p => p.User)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .ToListAsync();

            foreach (var task in projectTasks)
            {
                var taskDto = new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    DueDate = task.DueDate,
                    CreatedAt = task.CreatedAt,
                    Project = new ProjectSummaryDto
                    {
                        Id = task.Project.Id,
                        Name = task.Project.Name,
                        CreatedAt = task.Project.CreatedAt,
                        Owner = new UserSummaryDto
                        {
                            Id = task.Project.User.Id,
                            Name = task.Project.User.Name,
                            Email = task.Project.User.Email
                        }
                    },
                    Status = new TaskStatusSummaryDto
                    {
                        Name = task.Status.Name
                    },
                    Priority = new TaskPrioritySummaryDto
                    {
                        Name = task.Priority.Name
                    }
                };

                tasksDto.Add(taskDto);
            }

            return tasksDto;
        }

        public async Task<TaskDto?> GetTaskAsync(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Project)
                .ThenInclude(p => p.User)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task is null)
            {
                return null;
            }

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                Project = new ProjectSummaryDto
                {
                    Id = task.Project.Id,
                    Name = task.Project.Name,
                    CreatedAt = task.Project.CreatedAt,
                    Owner = new UserSummaryDto
                    {
                        Id = task.Project.User.Id,
                        Name = task.Project.User.Name,
                        Email = task.Project.User.Email
                    }
                },
                Status = new TaskStatusSummaryDto
                {
                    Name = task.Status.Name
                },
                Priority = new TaskPrioritySummaryDto
                {
                    Name = task.Priority.Name
                }
            };
        }

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var updateTask = await _context.Tasks.FindAsync(id);

            if (updateTask is null)
            {
                // Task doesn`t exist
                return false;
            }

            if (dto.Title is not null)
                updateTask.Title = dto.Title;

            if (dto.DueDate is not null)
                updateTask.DueDate = (DateOnly)dto.DueDate;

            if (dto.StatusId is not null)
                updateTask.StatusId = (int)dto.StatusId;

            if (dto.PriorityId is not null)
                updateTask.PriorityId = (int)dto.PriorityId;

            // Commit changes to a database
            await _context.SaveChangesAsync();

            _logger.LogInformation("Task with id {id} updated", id);

            // Task updated successfully
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var deleteTask = await _context.Tasks.FindAsync(id);

            if (deleteTask is null)
            {
                // Task doesn`t exist
                return false;
            }

            _context.Tasks.Remove(deleteTask);

            // Commit changes to a database
            await _context.SaveChangesAsync();

            _logger.LogWarning("Task with id {id} deleted", id);

            // Task deleted successfully
            return true;
        }

        public async Task<List<TaskDto>> GetProjectTasksByStatusAsync(int projectId, string status)
        {
            var tasksByStatusDto = new List<TaskDto>();

            var tasksByStatus = await _context.Tasks
                .Where(t => t.ProjectId == projectId && t.Status.Name == status)
                .Include(t => t.Project)
                .ThenInclude(p => p.User)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .ToListAsync();

            foreach (var  taskByStatus in tasksByStatus)
            {
                var taskDto = new TaskDto
                {
                    Id = taskByStatus.Id,
                    Title = taskByStatus.Title,
                    DueDate = taskByStatus.DueDate,
                    CreatedAt = taskByStatus.CreatedAt,
                    Project = new ProjectSummaryDto
                    {
                        Id = taskByStatus.Project.Id,
                        Name = taskByStatus.Project.Name,
                        CreatedAt = taskByStatus.Project.CreatedAt,
                        Owner = new UserSummaryDto
                        {
                            Id = taskByStatus.Project.User.Id,
                            Name = taskByStatus.Project.User.Name,
                            Email = taskByStatus.Project.User.Email
                        }
                    },
                    Status = new TaskStatusSummaryDto
                    {
                        Name = taskByStatus.Status.Name
                    },
                    Priority = new TaskPrioritySummaryDto
                    {
                        Name = taskByStatus.Priority.Name
                    }

                };

                tasksByStatusDto.Add(taskDto);
            }

            return tasksByStatusDto;
        }

        public async Task<List<TaskDto>> GetProjectTasksByPriorityAsync(int projectId, string priority)
        {
            var tasksByPriorityDto = new List<TaskDto>();

            var tasksByPriority = await _context.Tasks
                .Where(t => t.ProjectId == projectId && t.Priority.Name == priority)
                .Include(t => t.Project)
                .ThenInclude(p => p.User)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .ToListAsync();

            foreach (var taskByPriority in tasksByPriority)
            {
                var taskByPriorityDto = new TaskDto
                {
                    Id = taskByPriority.Id,
                    Title = taskByPriority.Title,
                    DueDate = taskByPriority.DueDate,
                    CreatedAt = taskByPriority.CreatedAt,
                    Project = new ProjectSummaryDto
                    {
                        Id = taskByPriority.Project.Id,
                        Name = taskByPriority.Project.Name,
                        CreatedAt = taskByPriority.Project.CreatedAt,
                        Owner = new UserSummaryDto
                        {
                            Id = taskByPriority.Project.User.Id,
                            Name = taskByPriority.Project.User.Name,
                            Email = taskByPriority.Project.User.Email
                        }
                    },
                    Status = new TaskStatusSummaryDto
                    {
                        Name = taskByPriority.Status.Name
                    },
                    Priority = new TaskPrioritySummaryDto
                    {
                        Name = taskByPriority.Priority.Name
                    }
                };

                tasksByPriorityDto.Add(taskByPriorityDto);
            }

            return tasksByPriorityDto;
        }

        public async Task<List<TaskDto>> GetProjectTasksByStatusAndPriorityAsync(int projectId, string status, string priority)
        {
            var tasksByStatusAndPriorityDto = new List<TaskDto>();

            var tasksByStatusAndPriority = await _context.Tasks
                .Where(t => t.ProjectId == projectId && (t.Status.Name == status && t.Priority.Name == priority))
                .Include(t => t.Project)
                .ThenInclude(p => p.User)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .ToListAsync();

            foreach (var taskByStatusAndPriority in tasksByStatusAndPriority)
            {
                var taskByStatusAndPriorityDto = new TaskDto
                {
                    Id = taskByStatusAndPriority.Id,
                    Title = taskByStatusAndPriority.Title,
                    DueDate = taskByStatusAndPriority.DueDate,
                    CreatedAt = taskByStatusAndPriority.CreatedAt,
                    Project = new ProjectSummaryDto
                    {
                        Id = taskByStatusAndPriority.Project.Id,
                        Name = taskByStatusAndPriority.Project.Name,
                        CreatedAt = taskByStatusAndPriority.Project.CreatedAt,
                        Owner = new UserSummaryDto
                        {
                            Id = taskByStatusAndPriority.Project.User.Id,
                            Name = taskByStatusAndPriority.Project.User.Name,
                            Email = taskByStatusAndPriority.Project.User.Email
                        }
                    },
                    Status = new TaskStatusSummaryDto
                    {
                        Name = taskByStatusAndPriority.Status.Name
                    },
                    Priority = new TaskPrioritySummaryDto
                    {
                        Name = taskByStatusAndPriority.Priority.Name
                    }
                };

                tasksByStatusAndPriorityDto.Add(taskByStatusAndPriorityDto);
            }

            return tasksByStatusAndPriorityDto;
        }

        public async Task<List<TaskDto>> GetProjectTasksByPageSizeAsync(int projectId, int page, int pageSize)
        {
            var tasksByPageSizeDto = new List<TaskDto>();

            var tasksByPageSize = await _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .OrderBy(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(t => t.Project)
                .ThenInclude(p => p.User)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .ToListAsync();

            foreach (var taskByPageSize in tasksByPageSize)
            {
                var taskByPageSizeDto = new TaskDto
                {
                    Id = taskByPageSize.Id,
                    Title = taskByPageSize.Title,
                    DueDate = taskByPageSize.DueDate,
                    CreatedAt = taskByPageSize.CreatedAt,
                    Project = new ProjectSummaryDto
                    {
                        Id = taskByPageSize.Project.Id,
                        Name = taskByPageSize.Project.Name,
                        CreatedAt = taskByPageSize.Project.CreatedAt,
                        Owner = new UserSummaryDto
                        {
                            Id = taskByPageSize.Project.User.Id,
                            Name = taskByPageSize.Project.User.Name,
                            Email = taskByPageSize.Project.User.Email
                        }
                    },
                    Status = new TaskStatusSummaryDto
                    {
                        Name = taskByPageSize.Status.Name
                    },
                    Priority = new TaskPrioritySummaryDto
                    {
                        Name = taskByPageSize.Priority.Name
                    }
                };

                tasksByPageSizeDto.Add(taskByPageSizeDto);
            }

            return tasksByPageSizeDto;
        }
    }
}