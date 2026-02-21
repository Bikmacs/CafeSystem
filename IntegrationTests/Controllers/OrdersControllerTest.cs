using System.Net.Http.Json;
using CafeAPI.DTOs.OrderItems;
using CafeAPI.DTOs.Orders;
using CafeAPI.Models;
using IntegrationTests.Infrastructure;

namespace IntegrationTests.Controllers;

[TestFixture]
public class OrdersControllerTest : BaseIntegrationTest
{
    private CreateOrderDto _createOrderDto;
    private OrderItemCreateDto _orderItemCreateDto;

    [SetUp]
    public void SetUp()
    {
        _orderItemCreateDto = new OrderItemCreateDto
        {
            MenuItemId = 1,
            Quantity = 1
        };
        _createOrderDto = new CreateOrderDto
        {
            UserId = 1,
            TableNumber = 1,
            Status = "Готовиться",
            Items = [_orderItemCreateDto]
        };
    }

    [Test]
    public async Task CreateOrderTest()
    {
        AuthenticateWaiterAsRole();
        
        var response = await HttpClient.PostAsJsonAsync("/api/Orders/CreateOrder", _createOrderDto);
        response.EnsureSuccessStatusCode();

        var orderInDb = Dbcontext.Orders.FirstOrDefault(o => o.OrderId == 1);
        
        Assert.That(orderInDb, Is.Not.Null);
        Assert.AreEqual(orderInDb.OrderId, 1);
        //сравнить данные 
    }

    [Test]
    public async Task GetAllOrdersTest()
    {
        AuthenticateWaiterAsRole();
        
        var response = await HttpClient.GetAsync("/api/Orders/GetAll");
        response.EnsureSuccessStatusCode();
        
        var ordersDb = Dbcontext.Orders.ToList();
        var ordersApi = await response.Content.ReadFromJsonAsync<List<OrderResponseDto>>();

        if (ordersApi != null) Assert.That(ordersApi.Count, Is.EqualTo(ordersDb.Count));
        
    }
    
    
}