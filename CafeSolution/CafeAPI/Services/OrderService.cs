using CafeAPI.DTOs.Kitchen;
using CafeAPI.DTOs.OrderItems;
using CafeAPI.DTOs.Orders;
using CafeAPI.Interfaces.IRepository;
using CafeAPI.Interfaces.IServices;
using CafeAPI.Models;
using ClosedXML.Excel;
using Microsoft.Identity.Client;

namespace CafeAPI.Services
{
    public class OrderService(
        IOrderRepository orderRepository,
        IMenuItemRepository menuItemRepository,
        IUserRepository userRepository,
        IOrderItemRepository itemRepository)
        : IOrderService
    {
        public async Task<bool> AddOrderItemsAsync(int orderId, CreateOrderDto itemsDto)
        {
            var order = await orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return false;
            if (order.Status is "Оплачен" or "Закрыт")
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
            var ordersDto = orders.Select(order => new OrderResponseDto
            {
                OrderId = order.OrderId,
                UserName = order.User?.FullName ?? "Неизвестно",
                UserId = order.UserId,
                TableNumber = order.TableNumber,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                TotalAmount = order.OrderItems?.Sum(oi => oi.UnitPrice * oi.Quantity) ?? 0
            }).ToList();
            return ordersDto;
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
            var currentCafeTime = DateTime.UtcNow.AddHours(5);
            var dateStart = isMonthly
                ? new DateTime(currentCafeTime.Year, currentCafeTime.Month, 1)
                : currentCafeTime.Date;
            var (reportOrders, total) = await GetReportDataAsync(isMonthly);

            using var workbook = new XLWorkbook();
            var ws1 = workbook.Worksheets.Add("Общая выручка");

            ws1.Cell("A1").Value = isMonthly ? "ЕЖЕМЕСЯЧНЫЙ ОТЧЕТ" : "ЕЖЕДНЕВНЫЙ ОТЧЕТ";
            ws1.Cell("A1").Style.Font.Bold = true;
            ws1.Cell("A1").Style.Font.FontSize = 14;

            ws1.Cell("A2").Value = "Период начала:";
            ws1.Cell("B2").Value = dateStart.ToShortDateString();

            ws1.Cell("A3").Value = "ИТОГО ВЫРУЧКА:";
            ws1.Cell("B3").Value = total;
            ws1.Cell("B3").Style.NumberFormat.Format = "#,##0.00\" ₽\"";
            ws1.Cell("B3").Style.Font.Bold = true;
            ws1.Cell("B3").Style.Font.FontColor = XLColor.DarkGreen;

            var headerRange = ws1.Range("A5:E5");
            ws1.Cell("A5").Value = "ID Заказа";
            ws1.Cell("B5").Value = "Дата и время";
            ws1.Cell("C5").Value = "Столик";
            ws1.Cell("D5").Value = "Официант";
            ws1.Cell("E5").Value = "Сумма";

            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            var currentRow = 6;
            foreach (var order in reportOrders)
            {
                ws1.Cell(currentRow, 1).Value = order.OrderId;
                ws1.Cell(currentRow, 2).Value = order.CreatedAt.ToString("g");
                ws1.Cell(currentRow, 3).Value = $"№ {order.TableNumber}";
                ws1.Cell(currentRow, 4).Value = order.User?.FullName ?? "Не указан";

                var orderSum = order.OrderItems?.Sum(oi => oi.UnitPrice * oi.Quantity) ?? 0;
                ws1.Cell(currentRow, 5).Value = orderSum;
                ws1.Cell(currentRow, 5).Style.NumberFormat.Format = "#,##0.00\" ₽\"";
                currentRow++;
            }

            ws1.Columns().AdjustToContents();

            var ws2 = workbook.Worksheets.Add("Статистика сотрудников");

            ws2.Cell("A1").Value = "ЭФФЕКТИВНОСТЬ ПЕРСОНАЛА";
            ws2.Cell("A1").Style.Font.Bold = true;
            ws2.Cell("A1").Style.Font.FontSize = 14;

            ws2.Cell("A3").Value = "ФИО Официанта";
            ws2.Cell("B3").Value = "Кол-во чеков";
            ws2.Cell("C3").Value = "Общая выручка";
            ws2.Cell("D3").Value = "Премия";
            ws2.Range("A3:D3").Style.Font.Bold = true;
            ws2.Range("A3:D3").Style.Fill.BackgroundColor = XLColor.PastelBlue;

            var staffStats = reportOrders
                .GroupBy(o => o.User?.FullName ?? "Неизвестно")
                .Select(g => new
                {
                    Name = g.Key,
                    Count = g.Count(),
                    Sum = g.Sum(o => o.OrderItems?.Sum(oi => oi.UnitPrice * oi.Quantity) ?? 0)
                })
                .OrderByDescending(s => s.Sum);

            var staffRow = 4;
            foreach (var stat in staffStats)
            {
                
                ws2.Cell(staffRow, 1).Value = stat.Name;
                ws2.Cell(staffRow, 2).Value = stat.Count;
                
                ws2.Cell(staffRow, 3).Value = stat.Sum;
                ws2.Cell(staffRow, 3).Style.NumberFormat.Format = "#,##0.00\" ₽\"";
                
                ws2.Cell(staffRow, 4).Value = stat.Sum * 0.01m;
                ws2.Cell(staffRow, 4).Style.NumberFormat.Format = "#,##0.00\" ₽\"";
                
                staffRow++;
            }

            ws2.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        private async Task<(List<Order> Orders, decimal TotalSum)> GetReportDataAsync(bool isMonthly)
        {
            // Корректно вычисляем дату начала с учетом смещения кафе (+5 часов), как в основном методе
            var currentCafeTime = DateTime.UtcNow.AddHours(5);
            var dateStart = isMonthly 
                ? new DateTime(currentCafeTime.Year, currentCafeTime.Month, 1) 
                : currentCafeTime.Date;
            
            var allOrders = await orderRepository.GetAllAsync();

            var reportOrders = allOrders
                .Where(o => 
                    !string.IsNullOrWhiteSpace(o.Status) && 
                    o.Status.Trim().Equals("Оплачен", StringComparison.OrdinalIgnoreCase) && 
                    o.CreatedAt >= dateStart)
                .OrderBy(o => o.CreatedAt)
                .ToList();

            var total = reportOrders.Sum(order =>
                order.OrderItems?.Sum(oi => (decimal)oi.UnitPrice * oi.Quantity) ?? 0m);

            return (reportOrders, total);
        }
    }
}