using CafeAPI.Models;

namespace CafeAPI.Interfaces.IServices
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
