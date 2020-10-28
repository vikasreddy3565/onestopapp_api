using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneStopApp.Interface;
using OneStopApp_Api.EntityFramework.ViewModel;

namespace OneStopApp_Api_Controllers
{
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly ILogger _logger;
        private readonly IAuthService _service;
        public AuthController(ILogger<AuthController> logger, IAuthService service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] JwtParameterViewModel parameters)
        {
            if (parameters == null || (parameters.GrantType == "refresh_token" && string.IsNullOrEmpty(parameters.RefreshToken)))
            {
                return new NotFoundResult();
            }

            if (parameters.GrantType == "password")
            {
                return new OkObjectResult(_service.DoPassword(parameters));
            }
            else if (parameters.GrantType == "refresh_token")
            {
                return new OkObjectResult(_service.DoRefreshToken(parameters));
            }

            return new OkObjectResult(new TokenResultViewModel
            {
                Code = "9000",
                Message = "bad request",
                Data = null
            });
        }
    }
}