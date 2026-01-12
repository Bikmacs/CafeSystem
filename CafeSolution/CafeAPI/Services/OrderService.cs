using CafeAPI.DTOs.Kitchen;
using CafeAPI.DTOs.OrderItems;
using CafeAPI.DTOs.Orders;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Interfaces.IServices;
using CafeAPI.Models;
using CafeAPI.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _itemRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        public OrderService(IOrderRepository orderRepository, IMenuItemRepository menuItemRepository, IOrderItemRepository itemRepository)
        {
            _orderRepository = orderRepository;
            _menuItemRepository = menuItemRepository;
            _itemRepository = itemRepository;
        }

        public async Task<bool> AddOrderItemsAsync(int orderId, CreateOrderDto itemsDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return false;
            if (order.Status == "Оплачен" || order.Status == "Закрыт") throw new Exception("Нельзя добавить блюда в закрытый заказ.");

            foreach (var item in itemsDto.Items)
            {
                var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(item.MenuItemId);

                if (menuItem == null || menuItem.Available == false)
                {
                    throw new Exception($"Блюдо с ID {item.MenuItemId} не найдено или в стоп-листе.");
                }

                var newOrderItem = new OrderItem
                {
                    OrderId = orderId,
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    UnitPrice = menuItem.Price
                    
                };

                await _orderRepository.AddOrderItemAsync(newOrderItem);
            }

            return true;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            //Создаем новый заказ
            var order = new Order
            {
                UserId = orderDto.UserId,
                TableNumber = orderDto.TableNumber,
                Status = orderDto.Status,
                CreatedAt = DateTime.UtcNow
            };
            //Добавляем блюда в заказ
            foreach (var item in orderDto.Items)
            {
                var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(item.MenuItemId);
                if(menuItem == null || menuItem.Available == false)
                {
                    throw new Exception($"Блюдо {item.MenuItemId} не найдено. или же в стоп листе");
                }
                var newOrderItem = new OrderItem
                {
                    UnitPrice = menuItem.Price,
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    MenuItem = menuItem
                };
                order.OrderItems.Add(newOrderItem);
            }
            await _orderRepository.CreateOrderAsync(order);
            // Формируем ответ
            var orderResponse = new OrderResponseDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                TableNumber = order.TableNumber,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                TotalAmount = order.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity),

                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    OrderId = oi.OrderId,
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    MenuItemName = oi.MenuItem?.Name ?? "Неизвестно"

                }).ToList()
            };
            return orderResponse;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return false;
          
            await _orderRepository.DeleteOrderAsync(order);
            return true;
        }

        public async Task<bool> DeleteOrderItemsAsync(int id, int orderItemId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return false;
            if (order.Status == "Оплачен" || order.Status == "Закрыт") return false;

            await _itemRepository.RemoveOrderItemAsync(id, orderItemId);
            return true;
        }

        public async Task<List<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            var orderDtos = orders.Select(order => new OrderResponseDto
            {
                OrderId = order.OrderId,
                UserName = order.User?.FullName ?? "Неизвестно",
                UserId = order.UserId,
                TableNumber = order.TableNumber,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                TotalAmount = order.OrderItems?.Sum(oi => oi.UnitPrice * oi.Quantity) ?? 0
            }).ToList();
            return orderDtos;
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null) return null;

            var orderResponse = new OrderResponseDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                TableNumber = order.TableNumber,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                TotalAmount = order.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity),
                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    OrderId = oi.OrderId,
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    MenuItemName = oi.MenuItem?.Name ?? ""
                }).ToList()
            };

            return orderResponse;
        }

        public async Task<List<OrderResponseDto>> GetOrdersByDateAsync(DateTime date)
        {
            var order = await _orderRepository.GetOrdersByDateAsync(date);
            if (order == null) return new List<OrderResponseDto>();
            
            var response = order.Select(o => new OrderResponseDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                TableNumber = o.TableNumber,
                CreatedAt = o.CreatedAt,
                Status = o.Status
            }).ToList();

            return response;

        }

        public async Task<List<OrderResponseDto>> GetOrdersByStatusAsync(string status)
        {
            var order = await _orderRepository.GetOrderByStatusAsync(status);
           
            var response = order.Select(o => new OrderResponseDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                TableNumber = o.TableNumber,
                CreatedAt = o.CreatedAt,
                Status = o.Status
            }).ToList();

            return response;
        }

        public async Task<List<OrderResponseDto>> GetOrdersByUserAsync(int userId)
        {
            var order = await _orderRepository.GetOrderByUserAsync(userId);
            var response = order.Select(o => new OrderResponseDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                TableNumber = o.TableNumber,
                CreatedAt = o.CreatedAt,
                Status = o.Status
            }).ToList();

            return response;

        }

        public async Task<OrderResponseDto?> GetOrderWithItemsAsync(int id)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(id);
            if (order == null) return null;

            var orderItemsDto = order.OrderItems.Select(oi => new OrderItemDto
            {
                OrderItemId = oi.OrderItemId,
                MenuItemId = oi.MenuItemId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                MenuItemName = oi.MenuItem?.Name ?? "Неизвестно"
            }).ToList();

            var response = new OrderResponseDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                UserName = order.User?.FullName ?? "",
                TableNumber = order.TableNumber,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                TotalAmount = order.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity),
                Items = orderItemsDto
            };

            return response;
        }

        public async Task<List<OrderResponseDto>> GetKitchenOrdersAsync()
        {
            var allOrders = await _orderRepository.GetAllAsync();

            var kitchenOrders = allOrders
                .Where(o => o.Status == "Открыт" || o.Status == "Создан" || o.Status == "Готовится")
                .OrderBy(o => o.CreatedAt)
                .ToList();

            var result = kitchenOrders.Select(order => new OrderResponseDto
            {
                OrderId = order.OrderId,
                TableNumber = order.TableNumber,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                UserName = order.User?.FullName ?? "Неизвестно",

                Items = order.OrderItems
                    .GroupBy(item => item.MenuItemId)
                    .Select(group => new OrderItemDto
                    {
                        MenuItemId = group.Key,
                        MenuItemName = group.First().MenuItem?.Name ?? "Неизвестно",
                        Quantity = group.Sum(item => item.Quantity),
                        UnitPrice = group.First().UnitPrice,
                        OrderItemId = group.First().OrderItemId
                    })
                    .ToList()
            }).ToList();

            return result;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if(order == null) return false;
            await _orderRepository.UpdateOrderStatus(orderId, status);
            return true;
        }
    }
}
