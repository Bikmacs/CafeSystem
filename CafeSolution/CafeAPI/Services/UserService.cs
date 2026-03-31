using CafeAPI.DTOs.Users;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Interfaces.IServices;
using CafeAPI.Models;

namespace CafeAPI.Services
{
    public class UserService(IUserRepository userRepository, ITokenService tokenService) : IUserService
    {
        public async Task<UserResponseDto?> CreateUserAsync(CreateUserDto createUserDto)
        {
            var existingUser = await userRepository.GetByUsernameAsync(createUserDto.Login);
            if (existingUser != null)
            {
                return null;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password); 

            var user = new User
            {
                FullName = createUserDto.FullName,
                Login = createUserDto.Login,
                PasswordHash = hashedPassword,
                RoleId = createUserDto.RoleId, 
                CreatedAt = DateTime.UtcNow
            };

            await userRepository.AddUser(user);

            return new UserResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Login = user.Login,
                RoleId = user.RoleId,
                CreatedAt = user.CreatedAt
            };


        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var userDelete = await userRepository.GetUserByIdAsync(id);
            if (userDelete == null) return false;

            await userRepository.DeleteUser(id);
            return true;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await userRepository.GetUsersAsync();

            return users.Select(user => new UserResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Login = user.Login,
                RoleId = user.RoleId,
                CreatedAt = user.CreatedAt
            });
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await userRepository.GetUserByIdAsync(id);

            return user != null ? new UserResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Login = user.Login,
                RoleId = user.RoleId,
                CreatedAt = user.CreatedAt
            } : null;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await userRepository.GetByUsernameAsync(loginUserDto.Login);
            if (user == null)
            {
                return null;
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash);
            if (!isValidPassword) 
            {
                return null;
            }
            string token = tokenService.CreateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                UserData = new UserResponseDto
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    Login = loginUserDto.Login,
                    RoleId = user.RoleId,
                    CreatedAt = user.CreatedAt
                }
            };
        }

        public async Task<UserResponseDto?> UpdateUserAsync(int id, CreateUserDto updateUserDto)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            user.FullName = updateUserDto.FullName;

            await userRepository.UpdateUser(user);

            return new UserResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Login = user.Login,
                RoleId = user.RoleId,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
