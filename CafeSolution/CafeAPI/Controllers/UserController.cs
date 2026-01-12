using CafeAPI.DTOs.Users;
using CafeAPI.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto loginDto)
        {
            var response = await _userService.LoginAsync(loginDto);
            if (response == null) return Unauthorized();
            return Ok(response);
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserResponseDto?>> CreateUserAsync([FromBody] CreateUserDto createUserDto)
        {
            var user = await _userService.CreateUserAsync(createUserDto);
            if (user == null)
            {
                return BadRequest("пользователь с таким логином уже существует ");
            }
            return Ok(user);
        }


        [Authorize(Roles = "1")]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpGet("{id}/GetUserId")]
        public async Task<ActionResult<UserResponseDto?>> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize(Roles = "1")]
        [HttpPatch("{id}/UpdateUser")]
        public async Task<ActionResult<UserResponseDto?>> UpdateUserAsync(int id, [FromBody] CreateUserDto updateUserDto)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, updateUserDto);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }
    }
}
