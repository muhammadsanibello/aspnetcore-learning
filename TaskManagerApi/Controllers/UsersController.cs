using TaskManagerApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Interfaces;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var createdUser = await _userService.CreateUserAsync(dto);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id}, createdUser);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDto dto)
        {
            var updateSuccess = await _userService.UpdateUserAsync(id, dto);

            if (!updateSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var deletionSuccess = await _userService.DeleteUserAsync(id);

            if (!deletionSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}