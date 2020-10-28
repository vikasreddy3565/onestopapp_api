

using System.Threading.Tasks;
using OneStopApp_Api.ViewModel;

namespace OneStopApp.Interface
{
    public interface IWeatherService
    {
         Task<WeatherViewModel> GetWeatherAsync(string Ip);
    }
}