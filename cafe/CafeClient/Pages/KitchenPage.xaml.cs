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
        private DispatcherTimer _dataRefreshTimer;
        private DispatcherTimer _clockTimer;

        public KitchenPage(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;

            _dataRefreshTimer = new DispatcherTimer();
            _dataRefreshTimer.Interval = TimeSpan.FromSeconds(10);
            _dataRefreshTimer.Tick += async (s, e) => await LoadKitchenOrders();

            // таймер часов
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
    }
}