using CafeAPI.DTOs.Orders; 
using CafeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CafeAPI.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<List<OrderResponseDto>> GetAllOrdersAsync();
        Task<OrderResponseDto?> GetOrderByIdAsync(int id);
        Task<OrderResponseDto?> GetOrderWithItemsAsync(int id);
        Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto orderDto);
        Task<bool> DeleteOrderAsync(int id);
        Task<List<OrderResponseDto>> GetOrdersByUserAsync(int userId);
        Task<List<OrderResponseDto>> GetOrdersByStatusAsync(string status);
        Task<List<OrderResponseDto>> GetOrdersByDateAsync(DateTime date);
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);
        Task<bool> AddOrderItemsAsync(int orderId, CreateOrderDto itemsDto);
        Task<bool> DeleteOrderItemsAsync(int id, int orderItemId);
        Task<List<OrderResponseDto>> GetKitchenOrdersAsync();

    }
}