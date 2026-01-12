using CafeAPI.DTOs.Users;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Interfaces.IServices;
using CafeAPI.Models;

namespace CafeAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserResponseDto?> CreateUserAsync(CreateUserDto createUserDto)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(createUserDto.Login);
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

            await _userRepository.AddUser(user);

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
            var userDelete = await _userRepository.GetUserByIdAsync(id);
            if (userDelete == null) return false;

            await _userRepository.DeleteUser(id);
            return true;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();

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
            var user = await _userRepository.GetUserByIdAsync(id);

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
            var user = await _userRepository.GetByUsernameAsync(loginUserDto.Login);
            if (user == null)
            {
                return null;
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash);
            if (!isValidPassword) 
            {
                return null;
            }
            string token = _tokenService.CreateToken(user);

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
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            user.FullName = updateUserDto.FullName;

            await _userRepository.UpdateUser(user);

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
