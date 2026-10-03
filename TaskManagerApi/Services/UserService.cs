using TaskManagerApi.Models;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Interfaces;

namespace TaskManagerApi.Services
{
    public class UserService : IUserService
    {
        private readonly TaskManagerDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(TaskManagerDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email
            };

            _context.Users.Add(user);

            // Commit changes to a database
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {id} created", user.Id);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var usersDto = new List<UserDto>();

            var users = await _context.Users
                .Include(u => u.Projects)
                  .ThenInclude(p => p.Tasks)
                        .ThenInclude(t => t.Status)
                .Include(u => u.Projects)
                    .ThenInclude(p => p.Tasks)
                        .ThenInclude(t => t.Priority)
                .ToListAsync();

            foreach (var user in users)
            {
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Projects = user.Projects.Select(p => new ProjectListDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CreatedAt = p.CreatedAt,
                        Tasks = p.Tasks.Select(t => new TaskSummaryDto
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
                    }).ToList()
                };

                usersDto.Add(userDto);
            }

            return usersDto;
        }

        public async Task<UserDto?> GetUserAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Projects)
                    .ThenInclude(p => p.Tasks)
                        .ThenInclude(t => t.Status)
                .Include(u => u.Projects)
                    .ThenInclude(p => p.Tasks)
                        .ThenInclude(t => t.Priority)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Projects = user.Projects.Select(p => new ProjectListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CreatedAt = p.CreatedAt,
                    Tasks = p.Tasks.Select(t => new TaskSummaryDto
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
                }).ToList()
            };
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var updateUser = await _context.Users.FindAsync(id);

            if (updateUser is null)
            {
                // User doesn`t exist
                return false;
            }

            if (dto.Name is not null)
                updateUser.Name = dto.Name;

            if (dto.Email is not null)
                updateUser.Email = dto.Email;

            // Commit changes to a database
            await _context.SaveChangesAsync();

            _logger.LogInformation("User with id {id} updated", id);

            // User updated successfully
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var deleteUser = await _context.Users.FindAsync(id);

            if (deleteUser is null)
            {
                // User doesn`t exist
                return false;
            }

            _context.Users.Remove(deleteUser);

            // Commit changes to a database
            await _context.SaveChangesAsync();

            _logger.LogWarning("User with id {id} deleted", id);

            // User deleted successfully
            return true;
        }
    }
}