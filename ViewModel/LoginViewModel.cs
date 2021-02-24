using System.Collections.Generic;
using OneStopApp_Api.Enums;

namespace OneStopApp_Api.EntityFramework.ViewModel
{
    public class LoginViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public AuthResult Result { get; set; }
        public bool PasswordMatch { get; set; }
        public int? UserStatusId { get; set; }
        public bool PasswordChangeRequired { get; set; }
        public bool PasswordExpired { get; set; }
        public string PasswordExpirationDate { get; set; }
    }
}