using CafeClient.DTOs;
using CafeClient.DTOs.Orders;
using CafeClient.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CafeClient.Pages
{
    public partial class OrderDetailsPage : Page, INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private readonly int _orderId;
        private OrderResponseDto? _currentOrder;

        public OrderResponseDto CurrentOrder
        {
            get => _currentOrder;
            set { _currentOrder = value; OnPropertyChanged(); }
        }

        public OrderDetailsPage(int orderId, OrderResponseDto order, ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            _orderId = orderId;
            CurrentOrder = order;
            this.DataContext = this;

            this.Loaded += async (s, e) => await LoadFullOrderDetails();
        }

        private async Task LoadFullOrderDetails()
        {
            try
            {
                var fullOrder = await _apiService.GetOrdersByIdAsync(_orderId);

                if (fullOrder != null)
                {
                    if (string.IsNullOrEmpty(fullOrder.UserName) && !string.IsNullOrEmpty(CurrentOrder.UserName))
                    {
                        fullOrder.UserName = CurrentOrder.UserName;
                    }
                    if (!fullOrder.TableNumber.HasValue && CurrentOrder.TableNumber.HasValue)
                    {
                        fullOrder.TableNumber = CurrentOrder.TableNumber;
                    }

                    CurrentOrder = fullOrder;

                    UpdateButtonsVisibility();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка обновления: {ex.Message}");
            }
        }

        private void UpdateButtonsVisibility()
        {
            bool isClosed = CurrentOrder.Status == "Оплачен" || CurrentOrder.Status == "Закрыт";

            if (isClosed)
            {
                if (BtnPay != null) BtnPay.Visibility = Visibility.Collapsed;
                if (BtnDeleteOrder != null) BtnDeleteOrder.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (BtnPay != null) BtnPay.Visibility = Visibility.Visible;
                if (BtnDeleteOrder != null) BtnDeleteOrder.Visibility = Visibility.Visible;
            }
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void Buy_Button(object sender, RoutedEventArgs e)
        {
            if (CurrentOrder.Status == "Оплачен" || CurrentOrder.Status == "Закрыт") return;

            var result = MessageBox.Show($"Закрыть заказ №{_orderId} и принять оплату?",
                                         "Оплата", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                bool success = await _apiService.UpdateOrderStatusAsync(_orderId, "Оплачен");
                if (success)
                {
                    MessageBox.Show("Заказ успешно оплачен и закрыт.");
                    await LoadFullOrderDetails(); 
                }
                else
                {
                    MessageBox.Show("Ошибка при закрытии заказа.");
                }
            }
        }

        private async void Edit_Button(object sender, RoutedEventArgs e)
        {
            string nextStatus = "";
            if (CurrentOrder.Status == "Создан" || CurrentOrder.Status == "Открыт") nextStatus = "Готовится";
            else if (CurrentOrder.Status == "Готовится") nextStatus = "Готов";
            else if (CurrentOrder.Status == "Готов")
            {
                MessageBox.Show("Заказ уже готов. Можно переходить к оплате.");
                return;
            }
            else return;

            bool success = await _apiService.UpdateOrderStatusAsync(_orderId, nextStatus);
            if (success)
            {
                await LoadFullOrderDetails();
            }
            else
            {
                MessageBox.Show("Не удалось обновить статус.");
            }
        }

        private async void Delete_Button(object sender, RoutedEventArgs e)
        {
            if (CurrentOrder.Status == "Оплачен" || CurrentOrder.Status == "Закрыт") return;

            var result = MessageBox.Show($"Вы уверены, что хотите ПОЛНОСТЬЮ удалить заказ №{_orderId}?",
                                         "Удаление заказа", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    bool success = await _apiService.DeleteOrderAsync(_orderId);
                    if (success)
                    {
                        MessageBox.Show("Заказ удален.");
                        NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось удалить заказ.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }


        private void DeleteItem_Button(object sender, RoutedEventArgs e) => DeleteOrderItem_Click(sender, e);

        private async void DeleteOrderItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button || button.DataContext is not OrderItemDto item) return;

            if (CurrentOrder.Status == "Оплачен" || CurrentOrder.Status == "Закрыт")
            {
                MessageBox.Show("Невозможно удалить элемент из закрытого заказа.");
                return;
            }

            var confirm = MessageBox.Show($"Удалить '{item.MenuItemName}'?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes) return;

            try
            {
                bool success = await _apiService.DeleteOrderItemAsync(_orderId, item.OrderItemId);
                if (success)
                {
                    await LoadFullOrderDetails();
                    MessageBox.Show("Элемент удалён.");
                }
                else MessageBox.Show("Ошибка удаления.");
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
        }

        private void AddBill_Button(object sender, RoutedEventArgs e)
        {
            var selected = CurrentOrder.Items.Where(i => i.IsSelected).ToList();
            if (!selected.Any())
            {
                MessageBox.Show("Пожалуйста, выберите хотя бы один элемент для создания счёта.");
                return;
            }

            var bill = new BillDto { Id = CurrentOrder.Bills.Count + 1 };

            CurrentOrder.Bills.Add(bill);

            foreach (var item in selected)
            {
                item.IsSelected = false;
                bill.Items.Add(item);
                CurrentOrder.Items.Remove(item);
            }

            OnPropertyChanged(nameof(CurrentOrder));

        }

        private void DeleteBill_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is BillDto bill)
            {
                foreach (var item in bill.Items)
                {
                    CurrentOrder.Items.Add(item);
                }

                CurrentOrder.Bills.Remove(bill);
            }
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e) => e.Handled = IsTextNumeric(e.Text);
        private static bool IsTextNumeric(string text) => new Regex("[^0-9]+").IsMatch(text);

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
