using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OneStopApp.Interface;
using OneStopApp_Api.ViewModel;

namespace OneStopApp.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _client;
        public WeatherService(HttpClient client)
        {
            _client = client;
        }

        public async Task<WeatherViewModel> GetWeatherAsync(string Ip)
        {
            var Result = new WeatherViewModel();
            string info = new WebClient().DownloadString("http://ipinfo.io/" + Ip);
            var Location = JsonConvert.DeserializeObject<IpInfoViewModel>(info);
            HttpResponseMessage Response = await _client.GetAsync("http://api.openweathermap.org/data/2.5/weather?zip=" + Location.Postal + "," + Location.Country + "&appid=9bb762d4c4e36f50eebcd1007efc83b6");
            if (Response.IsSuccessStatusCode)
            {
                var FormData = Response.Content.ReadAsStringAsync().Result;
                return Result = JsonConvert.DeserializeObject<WeatherViewModel>(FormData);
            }
            return null;

        }
    }
};