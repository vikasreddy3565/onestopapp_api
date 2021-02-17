namespace Dms.Services.ViewModel.Security
{
    public class ForgotUserNameViewModel
    {
        public string Email { get; set; }       
    }

    public class ForgotPasswordViewModel : ForgotUserNameViewModel
    {
        public string UserName { get; set; } 
         public string Answer { get; set; } 
    }

    public class ForgotUserNameChangePasswordViewModel
    {
         public string UserName { get; set; } 
         public string Password { get; set; } 
    }

   

}