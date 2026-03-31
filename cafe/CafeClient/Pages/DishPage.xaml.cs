using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CafeClient.DTOs;
using CafeClient.DTOs.Menu;

namespace CafeClient.Pages;

public partial class DishPage : Page
{
    public DishPage(MenuItemResponseDto dish)
    {
        InitializeComponent();
        this.DataContext = dish;
        
        if (!string.IsNullOrEmpty(dish.Image))
        {
            try
            {
                DishImage.Source = new BitmapImage(
                    new Uri($"http://localhost:8080/images/{dish.Image}")
                );
            }
            catch
            {
                // ignored
            } 
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (NavigationService is { CanGoBack: true })
        {
            NavigationService.GoBack();
        }
    }
}