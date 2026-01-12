using CafeAPI.Models;

namespace CafeAPI.Interfaces.IRepository
{
    public interface IRoleRepository
    {
        Task AddRoleAsync(Role role);
        Task DeleteRoleAsync(Role role);
        Task <Role?> GetRoleByIdAsync (string roleId);
        Task<Role?> GetRoleByNameAsync(string roleName);
        Task <List<Role>> GetAllRolesAsync();
        Task UpdateRoleAsync (Role role);
    }
}
