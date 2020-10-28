

using OneStopApp_Api.EntityFramework.ViewModel;

namespace OneStopApp.Interface
{
    public interface IAuthService
    {
        TokenResultViewModel DoPassword(JwtParameterViewModel model);
        TokenResultViewModel DoRefreshToken(JwtParameterViewModel model);
    }
}