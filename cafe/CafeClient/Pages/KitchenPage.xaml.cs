using CafeClient.DTOs.Orders;
using CafeClient.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CafeClient.Pages
{
    public partial class KitchenPage : Page
    {
        private readonly ApiService _apiService;

        private DispatcherTimer _orderTimer; 
        private DispatcherTimer _clockTimer; 

        public KitchenPage(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;

            _orderTimer = new DispatcherTimer();
            _orderTimer.Interval = TimeSpan.FromSeconds(10);
            _orderTimer.Tick += OrderTimer_Tick;

            _clockTimer = new DispatcherTimer();
            _clockTimer.Interval = TimeSpan.FromSeconds(1);
            _clockTimer.Tick += ClockTimer_Tick;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateClock();
            await LoadKitchenOrders();

            _orderTimer.Start();
            _clockTimer.Start();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _orderTimer.Stop();
            _clockTimer.Stop();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private async void OrderTimer_Tick(object sender, EventArgs e)
        {
            await LoadKitchenOrders();
        }

        private void UpdateClock()
        {
            if (ClockTextBlock != null)
            {
                ClockTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
            }
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void BackMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
            else
                NavigationService.Navigate(new MainPage(_apiService, false));
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}