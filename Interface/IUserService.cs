using Dms.Services.ViewModel.Security;
using OneStopApp_Api.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace OneStopApp.Interface
{
    public interface IUserService
    {
        RegisterUserViewModel CreateUser(RegisterUserViewModel model);
        Task<bool> CheckUserExists(string userName);
        Task<bool> CheckEmailAddress(string email);
        Task<ResponseMessageViewModel> ForgotUserName(string email);
        Task<ResponseMessageViewModel> ChangePassword(ForgotUserNameChangePasswordViewModel model);
        List<CountryViewModel> GetCountries();
    }
}