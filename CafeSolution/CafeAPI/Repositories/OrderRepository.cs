using CafeAPI.Data;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CafeDbContext _context;
        public OrderRepository(CafeDbContext context)
        {
            _context = context;
        }
        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem); 
            await _context.SaveChangesAsync();
        }

        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem) 
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<List<Order>> GetOrderByStatusAsync(string status)
        {
            return await _context.Orders
                .Where(o => o.Status == status)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrderByUserAsync(int id)
        {
            return await _context.Orders
                .Where(u => u.UserId == id)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByDateAsync(DateTime date)
        {
            return await _context.Orders
                .Where(o => o.CreatedAt.Date == date.Date) 
                .ToListAsync();
            
        }

        public async Task<Order?> GetOrderWithItemsAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.OrderId == id); 
        }

        public async Task UpdateOrderStatus(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if(order == null) throw new Exception("Order not found");

            order.Status = status;
            await _context.SaveChangesAsync();
        }
    }
}
