using System.ComponentModel;

namespace OneStopApp_Api.Enums
{
    public enum AuthResult : int
    {
        Success = 1000,
        InvalidUserNamePassword = 2000,
        AccountLocked = 3000,
        Failed = 4000
    }

    public enum UserStatusEnum : int
    {
        [Description("Active")]
        Active = 1,
        [Description("Inactive")]
        Inactive = 2,
        [Description("Locked")]
        Locked = 3,
        [Description("Expired")]
        Expired = 4,
        [Description("OnHold")]
        OnHold = 5
    }

    public enum ClientEnum
    {
        [Description("1B341AE5-AF8C-4111-B316-B4EC7EA258DC")]
        FrontEnd
    }
}