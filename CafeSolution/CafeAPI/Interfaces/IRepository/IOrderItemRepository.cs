using CafeAPI.Models;

namespace CafeAPI.Interfaces.IRepository
{
    public interface IOrderItemRepository
    {
        Task AddOrderItemAsync(OrderItem orderItem);
        Task<bool> RemoveOrderItemAsync(int id, int orderItemId);
        Task<List<OrderItem>> GetOrderItemsAsync();
        Task UpdateOrderQuantityAsync(int orderItemId,int quantity);
        Task<OrderItem?> GetOrderItemByIdAsync(int id);



    }
}
