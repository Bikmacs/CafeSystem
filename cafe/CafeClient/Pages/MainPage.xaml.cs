using CafeClient.DTOs;
using CafeClient.DTOs.Orders;
using CafeClient.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CafeClient.Pages
{
    public partial class MainPage : Page
    {
        private readonly ApiService _apiService;
        private List<MenuItemResponseDto> _allMenuItems = new();
        private ICollectionView _menuView;
        private string _searchText = "";
        private string _currentCategory = null;

        public MainPage(ApiService apiService, bool isCookies)
        {
            InitializeComponent();
            _apiService = apiService;

            if (!string.IsNullOrEmpty(CurrentUser.Token))
                _apiService.SetAuthorizationToken(CurrentUser.Token);

            if (!isCookies)
                ButtonUser.Visibility = Visibility.Collapsed;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var menu = await _apiService.GetMenuAsync();
            if (menu != null)
            {
                _allMenuItems = menu.ToList();
                _menuView = CollectionViewSource.GetDefaultView(_allMenuItems);
                _menuView.Filter = MenuFilter;
                MenuListView.ItemsSource = _menuView;
            }

            var orders = await _apiService.GetAllOrdersAsync();
            if (orders != null)
            {
                OrdersListView.ItemsSource = orders
                    .Where(o => o.Status != "Закрыт" && o.Status != "Оплачен")
                    .OrderByDescending(o => o.CreatedAt)
                    .ToList();

                HistoryListView.ItemsSource = orders
                    .Where(o => o.Status == "Закрыт" || o.Status == "Оплачен")
                    .OrderByDescending(o => o.CreatedAt)
                    .ToList();
            }
            else
            {
                MessageBox.Show("Не удалось загрузить заказы.");
            }
        }

        private bool MenuFilter(object obj)
        {
            if (obj is not MenuItemResponseDto item) return false;

            if (!string.IsNullOrEmpty(_searchText))
            {
                var text = _searchText.ToLowerInvariant();
                return (item.Name?.ToLowerInvariant().Contains(text) ?? false)
                    || (item.Description?.ToLowerInvariant().Contains(text) ?? false)
                    || (item.Category?.ToLowerInvariant().Contains(text) ?? false);
            }
            else if (!string.IsNullOrEmpty(_currentCategory))
            {
                return item.Category == _currentCategory;
            }

            return true;
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            _currentCategory = button?.Tag?.ToString();
            _menuView.Refresh();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            _searchText = textBox?.Text ?? "";
            _menuView.Refresh();
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти из системы?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                CurrentUser.Clear();
                _apiService.Logout();
                NavigationService.Navigate(new LoginPage(_apiService));
            }
        }

        
        private async void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            //Получаем выбранный пункт меню из контекста кнопки    
            var button = sender as Button;
            var menuItem = button?.DataContext as MenuItemResponseDto;
            if (menuItem == null) return;
            //Получаем выбранный заказ из списка заказов
            var selectedOrder = OrdersListView.SelectedItem as OrderResponseDto;
            if (selectedOrder == null)
            {
                MessageBox.Show("Сначала выберите активный заказ в списке слева!",
                    "Внимание", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            //Создаем DTO для добавления пункта в заказ
            var itemsToAdd = new List<CreateOrderItemDto>
            {
                new CreateOrderItemDto { MenuItemId = menuItem.MenuItemId, Quantity = 1 }
            };
            //Вызываем API для добавления пункта в заказ
            bool success = await _apiService.AddItemsToOrderAsync(selectedOrder.OrderId, itemsToAdd);
            if (success)
            {
                await LoadData();
                RestoreSelection(selectedOrder.OrderId);
            }
            else
            {
                MessageBox.Show("Не удалось добавить блюдо. Возможно, заказ закрыт или удален.");
            }
        }

        private void RestoreSelection(int orderId)
        {
            var orders = OrdersListView.ItemsSource as IEnumerable<OrderResponseDto>;
            var orderToSelect = orders?.FirstOrDefault(o => o.OrderId == orderId);
            if (orderToSelect != null)
                OrdersListView.SelectedItem = orderToSelect;
        }

        private void OrdersListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (OrdersListView.SelectedItem is not OrderResponseDto selectedOrder) return;
            NavigationService?.Navigate(new OrderDetailsPage(selectedOrder.OrderId, selectedOrder, _apiService));
        }

        private void HistoryListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (HistoryListView.SelectedItem is not OrderResponseDto selectedOrder) return;
            NavigationService?.Navigate(new OrderDetailsPage(selectedOrder.OrderId, selectedOrder, _apiService));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserPage(_apiService));
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateOrderPage(_apiService));
        }

        private void OpenKitchen_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KitchenPage(_apiService));
        }
    }
}
