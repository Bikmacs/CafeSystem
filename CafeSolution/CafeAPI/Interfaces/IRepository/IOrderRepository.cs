using CafeAPI.Models;

namespace CafeAPI.Interfaces.IRepository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task CreateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<List<Order>> GetOrderByUserAsync(int id);
        Task <List<Order>> GetOrderByStatusAsync(string status);
        Task <List<Order>> GetOrdersByDateAsync(DateTime date);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task<Order?> GetOrderWithItemsAsync(int id);
        Task UpdateOrderStatus(int orderId, string status);
    }
}
