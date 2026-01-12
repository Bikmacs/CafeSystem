using Microsoft.AspNetCore.Identity;

namespace CafeAPI.DTOs.Users
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public UserResponseDto UserData { get; set; } = new UserResponseDto();
    }
}
