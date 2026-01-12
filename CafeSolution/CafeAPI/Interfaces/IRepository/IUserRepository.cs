using CafeAPI.Models;

namespace CafeAPI.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task DeleteUser(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task <List<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task UpdateUser(User user);
    }
}
