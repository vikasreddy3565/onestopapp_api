using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneStopApp.Interface;

namespace OneStopApp_Api_Controllers
{
    [Produces("application/json")]
    [Route("api/weather")]
    public class WeatherController : Controller
    {
        private readonly ILogger _logger;
        private readonly IWeatherService _service;
        public WeatherController(ILogger<WeatherController> logger, IWeatherService service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeatherAsync([FromRoute] string id)
        {
            try
            {
                var Weather = await _service.GetWeatherAsync(id);
                if (Weather != null)
                    return new OkObjectResult(Weather);
                else
                    return new NotFoundObjectResult(null);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}