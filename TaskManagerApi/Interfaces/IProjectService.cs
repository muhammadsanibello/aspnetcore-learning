using TaskManagerApi.DTOs;

namespace TaskManagerApi.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto);
        Task<List<ProjectDto>> GetProjectsAsync();
        Task<ProjectDto?> GetProjectAsync(int id);
        Task<bool> UpdateProjectAsync(int id, UpdateProjectDto dto);
        Task<bool> DeleteProjectAsync(int id);
    }
}
