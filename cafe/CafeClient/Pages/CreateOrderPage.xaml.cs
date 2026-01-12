using CafeClient.DTOs.Orders;
using CafeClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CafeClient.Pages
{
    public partial class CreateOrderPage : Page, INotifyPropertyChanged
    {
        private readonly ApiService _apiService;

        private List<TableDto> _tables;
        public List<TableDto> Tables
        {
            get => _tables;
            set { _tables = value; OnPropertyChanged(); }
        }

        private TableDto _selectedTable;
        public TableDto SelectedTable
        {
            get => _selectedTable;
            set { _selectedTable = value; OnPropertyChanged(); }
        }

        public CreateOrderPage(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;

            DataContext = this;

            Loaded += CreateOrderPage_Loaded;
        }

        private async void CreateOrderPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadTablesAsync();
        }

        private async Task LoadTablesAsync()
        {
            var orders = await _apiService.GetAllOrdersAsync();

            var busyTableIds = new HashSet<int>();
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    if (order.Status != "Закрыт" && order.Status != "Оплачен")
                    {
                        if (order.TableNumber.HasValue)
                        {
                            busyTableIds.Add(order.TableNumber.Value);
                        }
                    }
                }
            }

            var generatedTables = new List<TableDto>();
            for (int i = 1; i <= 40; i++)
            {
                bool isBusy = busyTableIds.Contains(i);

                generatedTables.Add(new TableDto
                {
                    TableId = i,    
                    TableNumber = i,
                    IsBusy = isBusy,
                    DisplayName = $"Столик №{i}  —  " + (isBusy ? "ЗАНЯТ" : "свободен")
                });
            }

            Tables = generatedTables;
        }
        private async void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTable == null)
            {
                MessageBox.Show("Пожалуйста, выберите стол.");
                return;
            }

            if (SelectedTable.IsBusy)
            {
                MessageBox.Show($"Столик №{SelectedTable.TableNumber} уже занят!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dto = new CreateOrderDto
            {
                UserId = CurrentUser.UserId,
                TableNumber = SelectedTable.TableNumber,
                Status = "Открыт",
                Items = new List<CreateOrderItemDto>()
            };

            try
            {
                var result = await _apiService.CreateOrderAsync(dto);

                if (result != null)
                {
                    MessageBox.Show($"Заказ для столика №{SelectedTable.TableNumber} успешно создан!");
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Сервер не создал заказ (вернул null).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка соединения: " + ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}