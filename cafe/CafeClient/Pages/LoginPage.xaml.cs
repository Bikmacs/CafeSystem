using CafeClient.Services;
using CafeClient.Utilities; 
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CafeClient.Pages
{
    public partial class LoginPage : Page, INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private bool isCockies = false;
        private string _login = string.Empty;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(); 
            }
        }

        public ICommand LoginCommand { get; private set; }

        public LoginPage(ApiService apiService)
        {
            InitializeComponent();
            this.DataContext = this;
            _apiService = apiService;

            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        private async void ExecuteLogin(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox?.Password;

            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.");
                return;
            }

            var response = await _apiService.LoginAsync(Login, password);

            if (response != null)
            {
                CurrentUser.Set(response);
                _apiService.SetAuthorizationToken(CurrentUser.Token);
                MessageBox.Show($"Вход выполнен! Роль: {CurrentUser.RoleId}");
                if (CurrentUser.RoleId == 1)
                {
                    NavigationService.Navigate(new MainPage(_apiService, true));
                }
                else if (CurrentUser.RoleId == 2)
                {
                    NavigationService.Navigate(new MainPage(_apiService, false));
                }
                else
                {
                    NavigationService.Navigate(new KitchenPage(_apiService));
                }

            }
            else
            {
                MessageBox.Show("Ошибка авторизации. Проверьте данные.");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}