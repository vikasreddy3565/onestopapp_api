using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneStopApp.Interface;

namespace OneStopApp_Api_Controllers
{
    [Produces("application/json")]
    [Route("api/News")]  
    public class NewsController : Controller
    {
        private readonly ILogger _logger;
        private readonly ITechnologyService _technolgyService;
        public NewsController(ILogger<NewsController> logger, ITechnologyService technologyService) {
            this._logger = logger;
            this._technolgyService = technologyService;
        }
    }
}