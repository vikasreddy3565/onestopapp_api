namespace OneStopApp_Api.Interface
{
    public interface ISaltPasswordService
    {
        string SaltPassword(string plainTextPassword);
        bool ComparePasswords(string encryptedPassword, string plainTextPassword);
    }
}