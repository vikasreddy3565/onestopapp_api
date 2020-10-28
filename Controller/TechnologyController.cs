using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneStopApp.Interface;

namespace OneStopApp_Api_Controllers
{
    [Produces("application/json")]
    [Route("api/technology")]  
    public class TechnologyController : Controller
    {
        private readonly ILogger<TechnologyController> _logger;
        private readonly ITechnologyService _technolgyService;
        public TechnologyController(ILogger<TechnologyController> logger, ITechnologyService technologyService) {
            _logger = logger;
            _technolgyService = technologyService;
        }
    }
}