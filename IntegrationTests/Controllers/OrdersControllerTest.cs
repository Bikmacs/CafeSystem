using System.Net;
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
    private MenuItem _testMenuItem;
    private Order _existingOrder;
    private const string TargetStatus = "Готовится";


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
        _existingOrder = new Order
        {
            UserId = 1,
            TableNumber = 1,
            Status = "Готовится"
        };
        var testCategory = new Category { Name = "Чай" };
        _testMenuItem = new MenuItem
        {
            Name = "Черный чай",
            Price = 150,
            Description = "Авторский чай",
            Available = true,
            Category = testCategory
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

    [Test]
    public async Task AddItemsToOrder_ValidItems_AddsSuccessfully()
    {
        AuthenticateWaiterAsRole();

        var existingOrder = new Order { UserId = 1, TableNumber = 1, Status = "Готовится" };
        Dbcontext.Orders.Add(existingOrder);

        Dbcontext.MenuItems.Add(_testMenuItem);
        await Dbcontext.SaveChangesAsync();

        var requestDto = new CreateOrderDto
        {
            UserId = 1,
            TableNumber = 1,
            Status = "Готовится",
            Items = [new OrderItemCreateDto { MenuItemId = _testMenuItem.MenuItemId, Quantity = 2 }]
        };

        var response =
            await HttpClient.PostAsJsonAsync($"/api/Orders/{existingOrder.OrderId}/AddItemsToOrder", requestDto);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var errorBody = await response.Content.ReadAsStringAsync();
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Ошибка: {errorBody}");
        Dbcontext.ChangeTracker.Clear();

        var orderItem = Dbcontext.OrderItems
            .FirstOrDefault(o => o.OrderId == existingOrder.OrderId
                                 && o.MenuItemId == _testMenuItem.MenuItemId);

        Assert.That(orderItem, Is.Not.Null, "Товар не добавился в базу к заказу");
        Assert.That(orderItem.Quantity, Is.EqualTo(2));
    }

    [Test]
    public async Task DeleteItemsToOrder_ValidItems_sSuccessfully()
    {
        AuthenticateWaiterAsRole();

        Dbcontext.Orders.Add(_existingOrder);
        await Dbcontext.SaveChangesAsync();

        var response = await HttpClient.DeleteAsync($"/api/Orders/{_existingOrder.OrderId}/DeleteOrder");

        var orderInDb = Dbcontext.Orders.FirstOrDefault(o => o.OrderId == _existingOrder.OrderId);
        Assert.That(orderInDb, Is.Null);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public async Task GetUserOrder_ValidUserId_ReturnsOkWithOrder()
    {
        AuthenticateWaiterAsRole();
        const int userId = 1;

        Dbcontext.Orders.Add(_existingOrder);
        await Dbcontext.SaveChangesAsync();

        var response = await HttpClient.GetAsync($"/api/Orders/{userId}/userOrder");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var orderInDb = Dbcontext.Orders.FirstOrDefault(o => o.OrderId == _existingOrder.OrderId);
        var userOrders = await response.Content.ReadFromJsonAsync<List<OrderResponseDto>>();

        Assert.That(orderInDb, Is.Not.Null);
        Assert.That(orderInDb.UserId, Is.EqualTo(userId));
        Assert.That(userOrders != null && userOrders.Any(o => o.OrderId == _existingOrder.OrderId), Is.True,
            "Созданный заказ не найден в ответе API");
    }

    [Test]
    public async Task ResponseStatusOrder_ValidStatus_ReturnsOkWithOrder()
    {
        AuthenticateWaiterAsRole();

        var response = await HttpClient.GetAsync($"/api/Orders/{TargetStatus}/status");
        var responseApi = await response.Content.ReadFromJsonAsync<List<OrderResponseDto>>();

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseApi != null && responseApi.All(x => x.Status == TargetStatus), Is.True);
        });
    }
    
    [Test]
    public async Task UpdateStatusOrder_ValidStatus()
    {
        AuthenticateWaiterAsRole();
        Dbcontext.Orders.Add(_existingOrder);
        await Dbcontext.SaveChangesAsync();

        var newStatus = "Создан";

        var response = await HttpClient.PatchAsJsonAsync($"/api/Orders/{_existingOrder.OrderId}/statusUpdate", newStatus);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        Dbcontext.ChangeTracker.Clear();
        
        var orderInDb = Dbcontext.Orders.FirstOrDefault(o => o.OrderId == _existingOrder.OrderId);
        Assert.That(orderInDb?.Status, Is.EqualTo(newStatus));
    }
    
    [Test]
    public async Task DeleteOrderItem_ValidData()
    {
        AuthenticateWaiterAsRole();
        Dbcontext.Orders.Add(_existingOrder);
        Dbcontext.MenuItems.Add(_testMenuItem);

        var orderItem = new OrderItem
        {
            OrderId = _existingOrder.OrderId,
            MenuItemId = _testMenuItem.MenuItemId,
            Quantity = 2
        };
        Dbcontext.OrderItems.Add(orderItem);
        await Dbcontext.SaveChangesAsync();
        
        var response = await HttpClient.DeleteAsync($"/api/Orders/{_existingOrder.OrderId}/deleteItem?orderItemId={orderItem.OrderItemId}");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        Dbcontext.ChangeTracker.Clear();
        
        var itemInDb = Dbcontext.OrderItems.FirstOrDefault(o => o.OrderItemId == orderItem.OrderItemId);
        Assert.That(itemInDb, Is.Null, "Позиция заказа должна быть удалена");
        
        var orderInDb = Dbcontext.Orders.FirstOrDefault(o => o.OrderId == _existingOrder.OrderId);
        Assert.That(orderInDb, Is.Not.Null);
        
    }
}