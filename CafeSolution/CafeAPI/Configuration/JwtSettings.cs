namespace CafeAPI.Configuration
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = string.Empty;  // адрес, выдающий токен
        public string Audience { get; set; } = string.Empty; // адрес, для которого предназначен токен
        public string SecretKey { get; set; } = string.Empty; // ключ для шифрации
        public int TokenLifeTimeInMinutes { get; set; }
    }
}
