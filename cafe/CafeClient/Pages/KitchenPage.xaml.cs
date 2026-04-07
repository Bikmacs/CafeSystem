using CafeClient.DTOs;
using CafeClient.DTOs.Orders;
using CafeClient.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace CafeClient.Pages
{
    public partial class KitchenPage : Page
    {
        private readonly ApiService _apiService;
        private readonly DispatcherTimer _dataRefreshTimer;
        private readonly DispatcherTimer _clockTimer;

        public KitchenPage(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;

            _dataRefreshTimer = new DispatcherTimer();
            _dataRefreshTimer.Interval = TimeSpan.FromSeconds(10);
            _dataRefreshTimer.Tick += async (s, e) => await LoadKitchenOrders();

            _clockTimer = new DispatcherTimer();
            _clockTimer.Interval = TimeSpan.FromSeconds(1);
            _clockTimer.Tick += ClockTimer_Tick;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTopClock();
            await LoadKitchenOrders();

            _dataRefreshTimer.Start();
            _clockTimer.Start();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _dataRefreshTimer.Stop();
            _clockTimer.Stop();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            UpdateTopClock();
            if (KitchenListView.ItemsSource is IEnumerable<object> items)
            {
                foreach (var item in items)
                {
                    if (item is OrderResponseDto order)
                    {
                        order.UpdateTimeUI();
                    }
                }
            }
        }

        private void UpdateTopClock()
        {
            ClockTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private async Task LoadKitchenOrders()
        {
            try
            {
                var kitchenOrders = await _apiService.GetKitchenOrdersAsync();

                if (kitchenOrders != null)
                {
                    KitchenListView.ItemsSource = kitchenOrders;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки кухни: {ex.Message}");
            }
        }

        private void BackMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private async void ButtonState_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button || button.DataContext is not OrderResponseDto selectedOrder)
            {
                return;
            }

            string nextStatus = "";

            if (selectedOrder.Status == "Готовится") 
            {
                nextStatus = "Готов";
            }
            else if (selectedOrder.Status == "Готов")
            {
                MessageBox.Show("Заказ уже готов и ожидает выдачи/оплаты.");
                return;
            }
            
            var result = MessageBox.Show($"Изменить статус заказа №{selectedOrder.OrderId} на '{nextStatus}'?",
                "Обновление статуса", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    bool success = await _apiService.UpdateOrderStatusAsync(selectedOrder.OrderId, nextStatus);

                    if (success)
                    {
                        await LoadKitchenOrders();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось обновить статус на сервере.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }
        
        
    }
}