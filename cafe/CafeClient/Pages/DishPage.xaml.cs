using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CafeClient.DTOs.Menu;
using CafeClient.Services;
using Microsoft.Win32;

namespace CafeClient.Pages
{
    public partial class DishPage : Page
    {
        private readonly bool _isCookies;
        private readonly ApiService _apiService;
        private readonly MenuItemResponseDto _currentDish;
        
        public bool IsAdmin => _isCookies;

        public DishPage(MenuItemResponseDto dish, bool isCookies, ApiService apiService)
        {
            InitializeComponent();
            _currentDish = dish;
            _apiService = apiService;
            _isCookies = isCookies;

            this.DataContext = _currentDish;

            if (!string.IsNullOrEmpty(_currentDish.Image))
            {
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri($"http://localhost:8080/images/{_currentDish.Image}");
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    DishImage.Source = bitmap;
                }
                catch
                {
                    DishImage.Source = null;
                }
            }

            UpdateImageVisibility();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null && NavigationService.CanGoBack)
                NavigationService.GoBack();
        }


        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string localPath = openFileDialog.FileName;

                try
                {
                    var newImageUrl = await _apiService.UploadMenuImageAsync(_currentDish.MenuItemId, localPath);

                    if (!string.IsNullOrEmpty(newImageUrl))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(localPath);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();

                        DishImage.Source = bitmap;
                        _currentDish.Image = System.IO.Path.GetFileName(newImageUrl);

                        MessageBox.Show("Изображение успешно обновлено!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
                finally
                {
                    UpdateImageVisibility();
                }
            }
        }

        private void UpdateImageVisibility()
        {
            if (_isCookies && DishImage.Source == null)
            {
                ButtonImage.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonImage.Visibility = Visibility.Collapsed;
            }
        }
    }
}