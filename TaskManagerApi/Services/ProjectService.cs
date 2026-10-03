using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using TaskManagerApi.Interfaces;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services
{
    public class ProjectService : IProjectService
    {
        private readonly TaskManagerDbContext _context;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(TaskManagerDbContext context, ILogger<ProjectService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                UserId = dto.UserId
            };

            _context.Projects.Add(project);

            // Commit changes to a database
            await _context.SaveChangesAsync();

            _logger.LogInformation("Project {id} created", project.Id);

            await _context.Projects.Entry(project)
                .Reference(p => p.User)
                .LoadAsync();

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                CreatedAt = project.CreatedAt,
                Owner = new UserSummaryDto
                {
                    Id = project.User.Id,
                    Name = project.User.Name,
                    Email = project.User.Email
                }
            };
        }

        public async Task<List<ProjectDto>> GetProjectsAsync()
        {
            var projectsDto = new List<ProjectDto>();

            var projects = await _context.Projects
                .Include(p => p.User)
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.Status)
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.Priority)
                .ToListAsync();

            foreach (var project in projects)
            {
                var projectDto = new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    CreatedAt = project.CreatedAt,
                    Owner = new UserSummaryDto
                    {
                        Id = project.User.Id,
                        Name = project.User.Name,
                        Email = project.User.Email
                    },
                    Tasks = project.Tasks.Select(t => new TaskSummaryDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        DueDate = t.DueDate,
                        CreatedAt = t.CreatedAt,
                        Status = new TaskStatusSummaryDto
                        {
                            Name = t.Status.Name
                        },
                        Priority = new TaskPrioritySummaryDto
                        {
                            Name = t.Priority.Name
                        }
                    }).ToList()
                };

                projectsDto.Add(projectDto);
            }

            return projectsDto;
        }

        public async Task<ProjectDto?> GetProjectAsync(int id)
        {
            var project = await _context.Projects
                .Include(p => p.User)
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.Status)
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.Priority)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project is null)
            {
                return null;
            }

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                CreatedAt = project.CreatedAt,
                Owner = new UserSummaryDto
                {
                    Id = project.User.Id,
                    Name = project.User.Name,
                    Email = project.User.Email
                },
                Tasks = project.Tasks.Select(t => new TaskSummaryDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    DueDate = t.DueDate,
                    CreatedAt = t.CreatedAt,
                    Status = new TaskStatusSummaryDto
                    {
                        Name = t.Status.Name
                    },
                    Priority = new TaskPrioritySummaryDto
                    {
                        Name = t.Priority.Name
                    }
                }).ToList()
            };
        }

        public async Task<bool> UpdateProjectAsync(int id, UpdateProjectDto dto)
        {
            var updateProject = await _context.Projects.FindAsync(id);

            if (updateProject is null)
            {
                // Project doesn`t exist
                return false;
            }

            if (dto.Name is not null)
                updateProject.Name = dto.Name;

            if (dto.Description is not null)
                updateProject.Description = dto.Description;

            // Commit changes to a database
            await _context.SaveChangesAsync();

            _logger.LogInformation("Project with id {id} updated", id);

            // Project updated successfully
            return true;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var deleteProject = await _context.Projects.FindAsync(id);

            if (deleteProject is null)
            {
                // Project doesn`t exist
                return false;
            }

            _context.Projects.Remove(deleteProject);

            // Commit changes to a database
            await _context.SaveChangesAsync();

            _logger.LogWarning("Project with id {id} deleted", id);

            // Project deleted successfully
            return true;
        }
    }
}