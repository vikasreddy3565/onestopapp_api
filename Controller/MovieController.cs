using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneStopApp.Interface;

namespace OneStopApp_Api_Controllers
{
    [Produces("application/json")]
    [Route("api/movie")]
    public class MovieController : Controller
    {
        private readonly ILogger _logger;
        private readonly ITechnologyService _technolgyService;
        public MovieController(ILogger<MovieController> logger, ITechnologyService technologyService)
        {
            this._logger = logger;
            this._technolgyService = technologyService;
        }

    }
}