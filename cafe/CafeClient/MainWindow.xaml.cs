using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CafeClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
            MainFrame.Navigated += MainFrame_Navigated;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                var focusedElement = Keyboard.FocusedElement;
                if (focusedElement is not TextBox && focusedElement is not PasswordBox)
                {
                    e.Handled = true; 
                }
            }
        }

        public void Navigate(Page page)
        {
            MainFrame.Navigate(page);
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Content is Page page)
            {
                this.Title = page.Title; 
            }
        }
    }
}
