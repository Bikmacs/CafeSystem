using CafeClient.DTOs;
using CafeClient.DTOs.Category;
using CafeClient.DTOs.Menu;
using CafeClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CafeClient.Pages
{
    public partial class MenuPage : Page, INotifyPropertyChanged
    {
        private readonly ApiService _apiService;

        private CreateMenuItemDto _newMenuItem = new CreateMenuItemDto();
        public CreateMenuItemDto NewMenuItem
        {
            get => _newMenuItem;
            set { _newMenuItem = value; OnPropertyChanged(); }
        }

        private List<MenuItemResponseDto> _items;
        public List<MenuItemResponseDto> Items
        {
            get => _items;
            set { _items = value; OnPropertyChanged(); }
        }

        private List<CategoryDto> _categories;
        public List<CategoryDto> Categories
        {
            get => _categories;
            set { _categories = value; OnPropertyChanged(); }
        }


        public MenuPage(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            DataContext = this;

            Loaded += MenuPage_Loaded;
        }

        private async void MenuPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCategories();
            await LoadData();
        }

        private async Task LoadCategories()
        {
            try
            {
                Categories = await _apiService.GetCategoriesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки категорий: " + ex.Message);
            }
        }




        private async Task LoadData()
        {
            try
            {
                var menu = await _apiService.GetMenuAsync();
                if (menu != null)
                {
                    Items = menu.OrderByDescending(i => i.MenuItemId).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки меню: " + ex.Message);
            }
        }

        private async void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewMenuItem.Name) || NewMenuItem.Price <= 0 || NewMenuItem.CategoryId == 0)
            {
                MessageBox.Show("Пожалуйста, заполните Название, выберите Категорию и укажите Цену!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Regex.IsMatch(NewMenuItem.Name, @"^[a-zA-Zа-яА-ЯёЁ\s\-]+$"))
            {
                MessageBox.Show("Название блюда должно содержать только буквы!", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool success = await _apiService.AddMenuAsync(NewMenuItem);

            if (success)
            {
                MessageBox.Show("Блюдо успешно добавлено!");
                ClearForm();
                await LoadData();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении. Проверьте, существуют ли категории в базе данных.");
            }
        }

        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is MenuItemResponseDto item)
            {
                var result = MessageBox.Show($"Удалить '{item.Name}' из меню?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bool success = await _apiService.DeleteMenuItemAsync(item.MenuItemId);
                    if (success)
                    {
                        await LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось удалить блюдо.");
                    }
                }
            }
        }

        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            NewMenuItem = new CreateMenuItemDto { Available = true };
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}