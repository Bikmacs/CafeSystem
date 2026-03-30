using System.Net.Http.Json;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using CafeAPI.DTOs.Users;
using CafeAPI.Models;
using IntegrationTests.Infrastructure;

namespace IntegrationTests.Controllers;

[TestFixture]
[AllureNUnit]
[AllureEpic("Управление кафе")]
[AllureFeature("Меню")]
public class UserControllerTests : BaseIntegrationTest
{
    private CreateUserDto _user;
    private LoginUserDto _loginUser;

    [SetUp]
    public void SetUp()
    {
        _user = new CreateUserDto
        {
            FullName = "Максим",
            Login = "admin2",
            Password = "123",
            RoleId = 1,
        };

        _loginUser = new LoginUserDto
        {
            Login = "admin2",
            Password = "123",
        };
    }

    [Test]
    public async Task RegisterUser_AsAdmin_Success()
    {
        AuthenticateAdminAsRole();
        var response = await HttpClient.PostAsJsonAsync("/api/User/Register", _user);
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.That(response.IsSuccessStatusCode, Is.True,
            $"Сервер не смог зарегистрировать пользователя. Ошибка: {responseString}");
        Dbcontext.ChangeTracker.Clear();

        var userInDb = Dbcontext.Users.FirstOrDefault(x => x.Login == _user.Login);

        Assert.That(userInDb, Is.Not.Null, "Пользователь не был сохранен в базу данных");
        Assert.That(userInDb.Login, Is.EqualTo(_user.Login), "Логин сохранился не так");
        Assert.That(userInDb.RoleId, Is.EqualTo(_user.RoleId), "Роль назначена неверно");
        Assert.That(userInDb.PasswordHash, Is.Not.EqualTo(_user.Password),
            "ОПАСТНОСТЬ: Пароль сохранен в открытом виде без хэширования");
    }

    [Test]
    public async Task LoginUser_Success()
    {
        var response = await HttpClient.PostAsJsonAsync("/api/User/Register", _user);
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.That(response.IsSuccessStatusCode, Is.True,
            $"Сервер не смог зарегистрировать пользователя. Ошибка: {responseString}");
        Dbcontext.ChangeTracker.Clear();
        HttpClient.DefaultRequestHeaders.Authorization = null;

        var responseLogin = await HttpClient.PostAsJsonAsync("/api/User/Login", _loginUser);
        var responseLoginUser = await response.Content.ReadAsStringAsync();
        Assert.That(responseLogin.IsSuccessStatusCode, Is.True, $"Сервер отклонил вход. Ошибка: {responseLoginUser}");
        //Assert.That(responseLoginUser,Does.Contain("ey"), "Ответ не содержит JWT токена (токены начинаются с 'ey')");
    }
}