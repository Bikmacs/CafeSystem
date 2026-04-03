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
using System.Windows.Media;
using System.Windows.Navigation;

namespace CafeClient.Pages
{
    public partial class CreateOrderPage : Page, INotifyPropertyChanged
    {
        private readonly ApiService _apiService;

        private List<TableVisualItem> _mapTables;

        public List<TableVisualItem> MapTables
        {
            get => _mapTables;
            set
            {
                _mapTables = value;
                OnPropertyChanged();
            }
        }

        private struct TableConfig
        {
            public int Number;
            public double X;
            public double Y;
            public double Width;
            public double Height;
            public int CornerRadius;
        }


// --- ИНТЕРЕСНАЯ ПЛАНИРОВКА (40 столов) ---
        private readonly TableConfig[] _realWorldTableConfigs = new TableConfig[]
        {
            // === ЛЕВОЕ КРЫЛО ===
            // VIP-Кабинки у левой стены (Большие прямоугольные столы: 100x60)
            new TableConfig { Number = 1, X = 40, Y = 200, Width = 100, Height = 60, CornerRadius = 8 },
            new TableConfig { Number = 2, X = 40, Y = 270, Width = 100, Height = 60, CornerRadius = 8 },
            new TableConfig { Number = 3, X = 40, Y = 340, Width = 100, Height = 60, CornerRadius = 8 },
            new TableConfig { Number = 4, X = 40, Y = 410, Width = 100, Height = 60, CornerRadius = 8 },
            new TableConfig { Number = 5, X = 40, Y = 480, Width = 100, Height = 60, CornerRadius = 8 },
            new TableConfig { Number = 6, X = 40, Y = 550, Width = 100, Height = 60, CornerRadius = 8 },

            // Малые столы для двоих (Компактные квадраты: 50x50)
            new TableConfig { Number = 7, X = 160, Y = 205, Width = 50, Height = 50, CornerRadius = 8 },
            new TableConfig { Number = 8, X = 160, Y = 275, Width = 50, Height = 50, CornerRadius = 8 },
            new TableConfig { Number = 9, X = 160, Y = 345, Width = 50, Height = 50, CornerRadius = 8 },
            new TableConfig { Number = 10, X = 160, Y = 415, Width = 50, Height = 50, CornerRadius = 8 },
            new TableConfig { Number = 11, X = 160, Y = 485, Width = 50, Height = 50, CornerRadius = 8 },
            new TableConfig { Number = 12, X = 160, Y = 555, Width = 50, Height = 50, CornerRadius = 8 },

            // === ЦЕНТРАЛЬНЫЙ ЗАЛ (Шахматный порядок / Зигзаг) ===
            // Круглые столы (60x60, Радиус 30), расставленные со смещением для динамики
            // Ряд 1
            new TableConfig { Number = 13, X = 280, Y = 230, Width = 60, Height = 60, CornerRadius = 30 },
            new TableConfig { Number = 14, X = 390, Y = 230, Width = 60, Height = 60, CornerRadius = 30 },
            new TableConfig { Number = 15, X = 500, Y = 230, Width = 60, Height = 60, CornerRadius = 30 },
            new TableConfig { Number = 16, X = 610, Y = 230, Width = 60, Height = 60, CornerRadius = 30 },
            // Ряд 2 (Смещен вправо на 50px)
            new TableConfig { Number = 17, X = 330, Y = 320, Width = 60, Height = 60, CornerRadius = 30 },
            new TableConfig { Number = 18, X = 440, Y = 320, Width = 60, Height = 60, CornerRadius = 30 },
            new TableConfig { Number = 19, X = 550, Y = 320, Width = 60, Height = 60, CornerRadius = 30 },
            new TableConfig { Number = 20, X = 660, Y = 320, Width = 60, Height = 60, CornerRadius = 30 },
            // Ряд 3
            new TableConfig { Number = 21, X = 280, Y = 410, Width = 60, Height = 60, CornerRadius = 30 },
            new TableConfig { Number = 22, X = 390, Y = 410, Width = 60, Height = 60, CornerRadius = 30 },
            new TableConfig { Number = 23, X = 500, Y = 410, Width = 60, Height = 60, CornerRadius = 30 },
            new TableConfig { Number = 24, X = 610, Y = 410, Width = 60, Height = 60, CornerRadius = 30 },
            // Ряд 4 (Смещен вправо на 50px)
            new TableConfig { Number = 25, X = 330, Y = 500, Width = 60, Height = 60, CornerRadius = 30 },
            new TableConfig { Number = 26, X = 440, Y = 500, Width = 60, Height = 60, CornerRadius = 30 },
            new TableConfig { Number = 27, X = 550, Y = 500, Width = 60, Height = 60, CornerRadius = 30 },
            new TableConfig { Number = 28, X = 660, Y = 500, Width = 60, Height = 60, CornerRadius = 30 },

            // === ПРАВОЕ КРЫЛО ===
            // Малые столы для двоих
            new TableConfig { Number = 29, X = 770, Y = 205, Width = 50, Height = 50, CornerRadius = 8 },
            new TableConfig { Number = 30, X = 770, Y = 275, Width = 50, Height = 50, CornerRadius = 8 },
            new TableConfig { Number = 31, X = 770, Y = 345, Width = 50, Height = 50, CornerRadius = 8 },
            new TableConfig { Number = 32, X = 770, Y = 415, Width = 50, Height = 50, CornerRadius = 8 },
            new TableConfig { Number = 33, X = 770, Y = 485, Width = 50, Height = 50, CornerRadius = 8 },
            new TableConfig { Number = 34, X = 770, Y = 555, Width = 50, Height = 50, CornerRadius = 8 },

            // VIP-Кабинки у правой стены
            new TableConfig { Number = 35, X = 840, Y = 200, Width = 100, Height = 60, CornerRadius = 8 },
            new TableConfig { Number = 36, X = 840, Y = 270, Width = 100, Height = 60, CornerRadius = 8 },
            new TableConfig { Number = 37, X = 840, Y = 340, Width = 100, Height = 60, CornerRadius = 8 },
            new TableConfig { Number = 38, X = 840, Y = 410, Width = 100, Height = 60, CornerRadius = 8 },
            new TableConfig { Number = 39, X = 840, Y = 480, Width = 100, Height = 60, CornerRadius = 8 },
            new TableConfig { Number = 40, X = 840, Y = 550, Width = 100, Height = 60, CornerRadius = 8 },
        };

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

            var generatedVisuals = new List<TableVisualItem>();

            for (int i = 0; i < _realWorldTableConfigs.Length; i++)
            {
                var config = _realWorldTableConfigs[i];
                bool isBusy = busyTableIds.Contains(config.Number);

                generatedVisuals.Add(new TableVisualItem
                {
                    TableNumber = config.Number,
                    IsBusy = isBusy,
                    X = config.X,
                    Y = config.Y,
                    Width = config.Width,
                    Height = config.Height,
                    CornerRadius = config.CornerRadius,
                });
            }

            MapTables = generatedVisuals;
        }

        private async void Table_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is TableVisualItem clickedTable)
            {
                if (clickedTable.IsBusy)
                {
                    MessageBox.Show($"Столик №{clickedTable.TableNumber} уже занят!", "Внимание", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show($"Открыть стол №{clickedTable.TableNumber}?", "Новый заказ",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmResult == MessageBoxResult.Yes)
                {
                    await CreateOrderForTableAsync(clickedTable.TableNumber);
                }
            }
        }

        private async Task CreateOrderForTableAsync(int tableNumber)
        {
            var dto = new CreateOrderDto
            {
                UserId = CurrentUser.UserId,
                TableNumber = tableNumber,
                Status = "Готовится",
                Items = new List<CreateOrderItemDto>()
            };

            try
            {
                var result = await _apiService.CreateOrderAsync(dto);
                if (result != null)
                {
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка соединения: " + ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack) NavigationService.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class TableVisualItem
    {
        public int TableNumber { get; set; }
        public bool IsBusy { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int CornerRadius { get; set; }

        public Brush BackgroundColor => IsBusy
            ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3B3")) // Бледно-красный
            : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B3FFB3")); // Бледно-зеленый
    }
}