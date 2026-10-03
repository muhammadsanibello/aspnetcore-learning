using TaskManagerApi.DTOs;

namespace TaskManagerApi.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserDto dto);
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto?> GetUserAsync(int id);
        Task<bool> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
