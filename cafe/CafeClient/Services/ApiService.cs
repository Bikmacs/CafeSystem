using CafeClient.DTOs;
using CafeClient.DTOs.Category;
using CafeClient.DTOs.Menu;
using CafeClient.DTOs.Orders;
using CafeClient.DTOs.User;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;

namespace CafeClient.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        #region Endpoints

        private static class Endpoints
        {
            public const string Login = "api/User/Login";
            public const string Register = "api/User/Register";
            public const string GetAllUsers = "api/User";
            public const string GetMenu = "api/Menu/GetMenu";
            public const string AddMenu = "api/Menu/Add";
            public const string GetAllOrders = "api/Orders/GetAll";
            public const string CreateOrder = "api/Orders/CreateOrder";
            public const string GetKitchen = "api/Orders/GetActiveOrders";
            public const string GetCategories = "api/Menu/GetCategories";


            public static string DeleteUser(int id) => $"api/User/DeleteUser?id={id}";
            public static string UpdateUser(int id, int role) => $"api/User/{id}/UpdateUser?Role={role}";
            public static string GetOrderById(int id) => $"api/Orders/{id}/GetOrderById";
            public static string UpdateOrderStatus(int id) => $"api/Orders/{id}/statusUpdate";

            public static string DeleteOrderItem(int orderId, int itemId) =>
                $"api/Orders/{orderId}/deleteItem?orderItemId={itemId}";

            public static string AddItemsToOrder(int orderId) => $"api/Orders/{orderId}/AddItemsToOrder";
            public static string DeleteOrder(int id) => $"api/Orders/{id}/DeleteOrder";
            public static string DeleteMenuItem(int id) => $"api/Menu/{id}/DeleteItem";
            public static string ExportRevenue(bool monthly) => $"api/Orders/ExportRevenue?isMonthly={monthly}";
            public static string UploadMenuItemImage(int id) => $"api/Menu/{id}/image";
        }

        #endregion

        public ApiService()
        {
            var baseUrl = App.Configuration["Api:BaseUrl"]
                          ?? throw new InvalidOperationException("Api:BaseUrl не настроен в конфигурации.");

            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        #region Auth

        public async Task<LoginResponseDto?> LoginAsync(string login, string password)
        {
            var dto = new LoginUserDto { Login = login, Password = password };
            return await PostAndReadAsync<LoginUserDto, LoginResponseDto>(Endpoints.Login, dto);
        }

        public void SetAuthorizationToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            Debug.WriteLine($"[Auth] Token установлен: {token[..Math.Min(token.Length, 20)]}...");
        }

        public void Logout()
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            Debug.WriteLine("[Auth] Токен удалён.");
        }

        #endregion

        #region Users

        public async Task<List<UserResponseDto>?> GetAllUsersAsync()
            => await GetAsync<List<UserResponseDto>>(Endpoints.GetAllUsers);

        public async Task<bool> RegisterUserAsync(CreateUserDto dto)
            => await PostAsync(Endpoints.Register, dto);

        public async Task<bool> DeleteUserAsync(int userId)
            => await DeleteAsync(Endpoints.DeleteUser(userId));

        public async Task<bool> UpdateUserAsync(int userId, CreateUserDto dto, int roleId)
            => await PatchAsync(Endpoints.UpdateUser(userId, roleId), dto);

        #endregion

        #region Menu

        public async Task<List<MenuItemResponseDto>?> GetMenuAsync()
            => await GetAsync<List<MenuItemResponseDto>>(Endpoints.GetMenu);

        public async Task<bool> AddMenuAsync(CreateMenuItemDto dto)
            => await PostAsync(Endpoints.AddMenu, dto);

        public async Task<bool> DeleteMenuItemAsync(int id)
            => await DeleteAsync(Endpoints.DeleteMenuItem(id));

        public async Task<List<CategoryDto>?> GetCategoriesAsync()
            => await GetAsync<List<CategoryDto>>(Endpoints.GetCategories);

        #endregion

        #region Orders

        public async Task<List<OrderResponseDto>?> GetAllOrdersAsync()
            => await GetAsync<List<OrderResponseDto>>(Endpoints.GetAllOrders);

        public async Task<OrderResponseDto?> GetOrdersByIdAsync(int id)
            => await GetAsync<OrderResponseDto>(Endpoints.GetOrderById(id));

        public async Task<List<OrderResponseDto>?> GetKitchenOrdersAsync()
            => await GetAsync<List<OrderResponseDto>>(Endpoints.GetKitchen);

        public async Task<OrderResponseDto?> CreateOrderAsync(CreateOrderDto dto)
            => await PostAndReadAsync<CreateOrderDto, OrderResponseDto>(Endpoints.CreateOrder, dto);

        public async Task<bool> AddItemsToOrderAsync(int orderId, List<CreateOrderItemDto> newItems)
        {
            var dto = new CreateOrderDto
            {
                UserId = CurrentUser.UserId,
                TableNumber = 0,
                Status = "Update",
                Items = newItems
            };
            return await PostAsync(Endpoints.AddItemsToOrder(orderId), dto);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
            => await PatchAsync(Endpoints.UpdateOrderStatus(orderId), newStatus);

        public async Task<bool> DeleteOrderItemAsync(int orderId, int orderItemId)
            => await DeleteAsync(Endpoints.DeleteOrderItem(orderId, orderItemId));

        public async Task<bool> DeleteOrderAsync(int orderId)
            => await DeleteAsync(Endpoints.DeleteOrder(orderId));

        public async Task<byte[]?> ExportRevenueExcelAsync(bool isMonthly)
        {
            var url = Endpoints.ExportRevenue(isMonthly);
            LogRequest(url);
            return await SafeExecuteAsync(async () =>
            {
                var response = await _httpClient.GetAsync(url);
                return response.IsSuccessStatusCode
                    ? await response.Content.ReadAsByteArrayAsync()
                    : null;
            }, "СКАЧИВАНИЕ ОТЧЁТА");
        }

        #endregion

        #region HTTP Helpers

        private async Task<T?> GetAsync<T>(string url)
        {
            LogRequest(url);
            return await SafeExecuteAsync(
                () => _httpClient.GetFromJsonAsync<T>(url),
                $"GET {url}");
        }

        private async Task<bool> PostAsync<T>(string url, T body)
        {
            return await SafeExecuteAsync(async () =>
                {
                    var response = await _httpClient.PostAsJsonAsync(url, body);
                    return response.IsSuccessStatusCode;
                }, $"POST {url}");
        }

        private async Task<TResponse?> PostAndReadAsync<TRequest, TResponse>(string url, TRequest body)
        {
            LogRequest(url);
            return await SafeExecuteAsync(async () =>
                {
                    var response = await _httpClient.PostAsJsonAsync(url, body);
                    return response.IsSuccessStatusCode
                        ? await response.Content.ReadFromJsonAsync<TResponse>()
                        : default;
                }, $"POST {url}");
        }

        private async Task<bool> PatchAsync<T>(string url, T body)
        {
            return await SafeExecuteAsync(async () =>
                {
                    var response = await _httpClient.PatchAsJsonAsync(url, body);
                    return response.IsSuccessStatusCode;
                }, $"PATCH {url}");
        }

        private async Task<bool> DeleteAsync(string url)
        {
            return await SafeExecuteAsync(async () =>
                {
                    var response = await _httpClient.DeleteAsync(url);
                    return response.IsSuccessStatusCode;
                }, $"DELETE {url}");
        }

        private static async Task<T?> SafeExecuteAsync<T>(Func<Task<T?>> action, string context)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ApiService] Ошибка [{context}]: {ex.Message}");
                return default;
            }
        }

        public async Task<string?> UploadMenuImageAsync(int menuItemId, string filePath)
        {
            var url = Endpoints.UploadMenuItemImage(menuItemId);
            LogRequest(url);

            return await SafeExecuteAsync(async () =>
            {
                using var content = new MultipartFormDataContent();

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var streamContent = new StreamContent(fileStream);

                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                content.Add(streamContent, "file", Path.GetFileName(filePath));

                var response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ImageUploadResponse>();
                    return result?.ImageUrl;
                }

                return null;
            }, "ЗАГРУЗКА ИЗОБРАЖЕНИЯ");
        }

        public class ImageUploadResponse
        {
            public string ImageUrl { get; set; }
        }

        private void LogRequest(string endpoint)
        {
            Debug.WriteLine($"[ApiService] --> {endpoint} | Auth: {_httpClient.DefaultRequestHeaders.Authorization}");
        }

        #endregion
    }
}