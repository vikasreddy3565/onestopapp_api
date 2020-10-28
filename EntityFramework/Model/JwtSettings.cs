using System.Collections.Generic;

namespace OneStopApp_Api.EntityFramework.ViewModel
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string AccessTokenExpirationTime { get; set; }
        public string RefreshTokenExpirationTime { get; set; }
    }
}