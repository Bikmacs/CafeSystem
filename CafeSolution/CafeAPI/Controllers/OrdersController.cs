using CafeAPI.DTOs.Orders;
using CafeAPI.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "1,2,3")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost("CreateOrder")]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder([FromBody] CreateOrderDto dto)
        {
            if (dto == null)
                return BadRequest("Тело запроса пустое");

            if (dto.UserId <= 0)
                return BadRequest("Некорректный UserId");

            if (dto.TableNumber is <= 0)
                return BadRequest("Некорректный номер столика");

            try
            {
                var createdOrder = await _orderService.CreateOrderAsync(dto);

                return CreatedAtAction(
                    nameof(GetOrderById),
                    new { id = createdOrder.OrderId },
                    createdOrder
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ошибка сервера при создании заказа");
            }
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}/GetOrderById")]
        public async Task<ActionResult<OrderResponseDto>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderWithItemsAsync(id);

            if (order == null)
            {
                return NotFound($"Заказ с ID {id} не найден.");
            }

            return Ok(order);
        }

        [HttpPost("{id}/AddItemsToOrder")]
        public async Task<IActionResult> AddItemsToOrder(int id, [FromBody] CreateOrderDto itemsDto)
        {
            try
            {
                var result = await _orderService.AddOrderItemsAsync(id, itemsDto);

                if (!result)
                {
                    return NotFound($"Заказ с ID {id} не найден.");
                }

                return Ok("Блюда успешно добавлены в заказ.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);

            if (!result)
            {
                return NotFound($"Заказ с ID {id} не найден.");
            }
            return NoContent();
        }


        [HttpGet("{userId}/userOrder")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrdersByUser(int userId)
        {
            var orders = await _orderService.GetOrdersByUserAsync(userId);
            return Ok(orders);
        }


        [HttpGet("{status}/status")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrdersByStatus(string status)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }


        [HttpPatch("{id}/statusUpdate")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus)
        {
            var result = await _orderService.UpdateOrderStatusAsync(id, newStatus);
            if (!result) return NotFound();
            return Ok($"Статус заказа {id} обновлен на '{newStatus}'");
        }

        [HttpDelete("{id}/deleteItem")]
        public async Task<IActionResult> DeleteOrderItems(int id, [FromQuery] int orderItemId)
        {
            try
            {
                var result = await _orderService.DeleteOrderItemsAsync(id, orderItemId);
                if (!result)
                {
                    return NotFound($"Заказ с ID {id} или позиция заказа с ID {orderItemId} не найдены.");
                }
                return Ok("Позиция заказа успешно удалена.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetActiveOrders")]
        public async Task<ActionResult<List<OrderResponseDto>>> GetActiveOrders()
        {
            try
            {
                var orders = await _orderService.GetKitchenOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка получения заказов кухни: {ex.Message}");
            }
        }

    }
}