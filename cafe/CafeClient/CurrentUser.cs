using CafeClient.DTOs.User;

namespace CafeClient
{
    public static class CurrentUser
    {
        public static string Token { get; set; } = string.Empty;
        public static int UserId { get; set; }
        public static int RoleId { get; set; }
        public static string FullName { get; set; } = string.Empty;

        public static void Set(LoginResponseDto data)
        {
            Token = data.Token;
            UserId = data.UserData.UserId;
            RoleId = data.UserData.RoleId;
            FullName = data.UserData.FullName;
        }

        public static void Clear()
        {
            Token = string.Empty;
            UserId = 0;
            RoleId = 0;
            FullName = string.Empty;
        }
        public static bool IsLoggedIn => !string.IsNullOrEmpty(Token);
    }
}