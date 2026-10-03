using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.DTOs;

namespace TaskManagerApi.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto> CreateTaskAsync(int projectId, CreateTaskDto dto);
        Task<List<TaskDto>> GetProjectTasksAsync(int projectId);
        Task<TaskDto?> GetTaskAsync(int id);
        Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto);
        Task<bool> DeleteTaskAsync(int id);
        Task<List<TaskDto>> GetProjectTasksByStatusAsync(int projectId, string status);
        Task<List<TaskDto>> GetProjectTasksByPriorityAsync(int projectId, string priority);
        Task<List<TaskDto>> GetProjectTasksByStatusAndPriorityAsync(int projectId, string status, string priority);
        Task<List<TaskDto>> GetProjectTasksByPageSizeAsync(int projectId, int page, int pageSize);
    }
}
