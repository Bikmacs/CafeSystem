using CafeAPI.Data;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeAPI.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly CafeDbContext _context;

        public OrderItemRepository(CafeDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderItem?> GetOrderItemByIdAsync(int id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<List<OrderItem>> GetOrderItemsAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }



        public async Task<bool> RemoveOrderItemAsync(int orderId, int orderItemId)
        {
            var item = await _context.OrderItems
                                     .FirstOrDefaultAsync(x => x.OrderItemId == orderItemId && x.OrderId == orderId);

            if (item == null)
                return false; 

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task UpdateOrderQuantityAsync(int orderItemId, int quantity)
        {
            var item = await _context.OrderItems.FindAsync(orderItemId);
            if(item != null)
            {
                item.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
