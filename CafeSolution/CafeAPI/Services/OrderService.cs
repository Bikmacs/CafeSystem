using CafeAPI.DTOs.Kitchen;
using CafeAPI.DTOs.OrderItems;
using CafeAPI.DTOs.Orders;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Interfaces.IServices;
using CafeAPI.Models;
using ClosedXML.Excel;

namespace CafeAPI.Services
{
    public class OrderService(
        IOrderRepository orderRepository,
        IMenuItemRepository menuItemRepository,
        IOrderItemRepository itemRepository)
        : IOrderService
    {
        public async Task<bool> AddOrderItemsAsync(int orderId, CreateOrderDto itemsDto)
        {
            var order = await orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return false;
            if (order.Status == "Оплачен" || order.Status == "Закрыт")
                throw new Exception("Нельзя добавить блюда в закрытый заказ.");

            foreach (var item in itemsDto.Items)
            {
                var menuItem = await menuItemRepository.GetMenuItemByIdAsync(item.MenuItemId);

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

                await orderRepository.AddOrderItemAsync(newOrderItem);
            }

            return true;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            var order = new Order
            {
                UserId = orderDto.UserId,
                TableNumber = orderDto.TableNumber,
                Status = orderDto.Status,
                CreatedAt = DateTime.UtcNow.AddHours(5)
            };
            foreach (var item in orderDto.Items)
            {
                var menuItem = await menuItemRepository.GetMenuItemByIdAsync(item.MenuItemId);
                if (menuItem == null || menuItem.Available == false)
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

            await orderRepository.CreateOrderAsync(order);
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
                    MenuItemName = oi.MenuItem.Name
                }).ToList()
            };
            return orderResponse;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await orderRepository.GetOrderByIdAsync(id);
            if (order == null) return false;

            await orderRepository.DeleteOrderAsync(order);
            return true;
        }

        public async Task<bool> DeleteOrderItemsAsync(int id, int orderItemId)
        {
            var order = await orderRepository.GetOrderByIdAsync(id);
            if (order == null) return false;
            if (order.Status == "Оплачен" || order.Status == "Закрыт") return false;

            await itemRepository.RemoveOrderItemAsync(id, orderItemId);
            return true;
        }

        public async Task<List<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await orderRepository.GetAllAsync();
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
            var order = await orderRepository.GetOrderByIdAsync(id);
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
            var order = await orderRepository.GetOrdersByDateAsync(date);
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
            var order = await orderRepository.GetOrderByStatusAsync(status);

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
            var order = await orderRepository.GetOrderByUserAsync(userId);
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
            var order = await orderRepository.GetOrderWithItemsAsync(id);
            if (order == null) return null;

            var orderItemsDto = order.OrderItems.Select(oi => new OrderItemDto
            {
                OrderItemId = oi.OrderItemId,
                MenuItemId = oi.MenuItemId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                MenuItemName = oi.MenuItem.Name
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
            var allOrders = await orderRepository.GetAllAsync();

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
            var order = await orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return false;
            await orderRepository.UpdateOrderStatus(orderId, status);
            return true;
        }

        public async Task<byte[]> ViruchkaShowExcel(bool isMonthly)
        {
            var dateStart = isMonthly
                ? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
                : DateTime.Today;

            var allOrders = await orderRepository.GetAllAsync();

            var reportOrders = allOrders
                .Where(st => st.Status == "Оплачен" && st.CreatedAt >= dateStart)
                .OrderBy(st => st.CreatedAt)
                .ToList();

            var total = reportOrders
                .Select(r => r.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity)).Sum();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Отчет по выручке");

            worksheet.Cell("A1").Value = isMonthly ? "Отчет за месяц:" : "Отчет за день:";
            worksheet.Cell("B1").Value = dateStart.ToShortDateString();

            worksheet.Cell("A2").Value = "Итоговая выручка:";
            worksheet.Cell("B2").Value = total;
            worksheet.Cell("B2").Style.NumberFormat.Format = "#,##0.00\" ₽\"";
            worksheet.Cell("A1").Style.Font.Bold = true;
            worksheet.Cell("A2").Style.Font.Bold = true;

            worksheet.Cell("A4").Value = "ID Заказа";
            worksheet.Cell("B4").Value = "Время";
            worksheet.Cell("C4").Value = "Столик";
            worksheet.Cell("D4").Value = "Сумма";
            worksheet.Range("A4:D4").Style.Font.Bold = true;
            worksheet.Range("A4:D4").Style.Fill.BackgroundColor = XLColor.LightGray;

            var currentRow = 5;
            foreach (var order in reportOrders)
            {
                worksheet.Cell(currentRow, 1).Value = order.OrderId;
                worksheet.Cell(currentRow, 2).Value = order.CreatedAt.ToString("HH:mm");
                worksheet.Cell(currentRow, 3).Value = order.TableNumber;

                var orderSum = order.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity);
                worksheet.Cell(currentRow, 4).Value = orderSum;
                worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = "#,##0.00\" ₽\"";

                currentRow++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}