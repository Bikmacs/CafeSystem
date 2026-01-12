using CafeClient.DTOs;
using CafeClient.DTOs.User;
using CafeClient.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CafeClient.Pages
{
    public partial class UserPage : Page, INotifyPropertyChanged
    {
        private readonly ApiService _apiService;

        private int? _selectedUserId = null;

        private CreateUserDto _editableUser = new CreateUserDto();
        public CreateUserDto EditableUser
        {
            get => _editableUser;
            set
            {
                _editableUser = value;
                OnPropertyChanged();
            }
        }

        private List<SimpleRole> _roles;
        public List<SimpleRole> Roles
        {
            get => _roles;
            set { _roles = value; OnPropertyChanged(); }
        }


        public UserPage(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            DataContext = this;

            Roles = new List<SimpleRole>
            {
                new SimpleRole { Id = 1, Name = "Администратор" },
                new SimpleRole { Id = 2, Name = "Официант" },
                new SimpleRole { Id = 3, Name = "Кухня" }
            };

            Loaded += UserPage_Loaded;
        }


        private async void UserPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            try
            {
                var users = await _apiService.GetAllUsersAsync();
                UsersListView.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EditableUser.Password = TxtPassword.Password;

            if (string.IsNullOrWhiteSpace(EditableUser.FullName) ||
                string.IsNullOrWhiteSpace(EditableUser.Login) ||
                EditableUser.RoleId <= 0)
            {
                MessageBox.Show("Пожалуйста, заполните ФИО и Логин!");
                return;
            }

            try
            {
                bool success = false;

                if (_selectedUserId.HasValue)
                {
                    success = await _apiService.UpdateUserAsync(_selectedUserId.Value, EditableUser, EditableUser.RoleId);

                    if (success) MessageBox.Show("Данные сотрудника обновлены!");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(EditableUser.Password))
                    {
                        MessageBox.Show("Для нового сотрудника пароль обязателен!");
                        return;
                    }

                    success = await _apiService.RegisterUserAsync(EditableUser);
                    if (success) MessageBox.Show("Новый сотрудник добавлен!");
                }

                if (success)
                {
                    ClearInputs(); 
                    await LoadUsers(); 
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении. Возможно, логин занят или нет прав.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (UsersListView.SelectedItem is not UserResponseDto selectedUser)
            {
                MessageBox.Show("Выберите сотрудника из списка для удаления.");
                return;
            }

            var result = MessageBox.Show($"Удалить сотрудника {selectedUser.FullName}?",
                                         "Подтверждение",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    bool success = await _apiService.DeleteUserAsync(selectedUser.UserId);
                    if (success)
                    {
                        await LoadUsers();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось удалить пользователя.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersListView.SelectedItem is UserResponseDto selectedUser)
            {
                _selectedUserId = selectedUser.UserId;

                EditableUser = new CreateUserDto
                {
                    FullName = selectedUser.FullName,
                    Login = selectedUser.Login,
                    Password = "" 
                };

                TxtPassword.Clear();
            }
        }

        private void TxtRole_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void ClearInputs()
        {
            _selectedUserId = null; 
            EditableUser = new CreateUserDto(); 
            TxtPassword.Clear();

            UsersListView.SelectedItem = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}