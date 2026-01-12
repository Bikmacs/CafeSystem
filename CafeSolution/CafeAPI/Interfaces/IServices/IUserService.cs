using CafeAPI.DTOs.Users;
using CafeAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CafeAPI.Interfaces.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto?> GetUserByIdAsync(int id);
        Task<UserResponseDto?> CreateUserAsync(CreateUserDto userDto);
        Task<UserResponseDto?> UpdateUserAsync(int id, CreateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(int id);
        Task<LoginResponseDto?> LoginAsync(LoginUserDto loginUserDto);
    }
}