using System.Threading.Tasks;
using Dms.Services.ViewModel.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneStopApp.Interface;
using OneStopApp_Api.ViewModel;

namespace Dms.Api.Designee.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController
    {
        private readonly IUserService _registerUserService;
        private readonly ILogger _logger;
        public UserController(ILogger<UserController> logger, IUserService registerUserServices)
        {
            _registerUserService = registerUserServices;
            _logger = logger;
        }

        [HttpGet("CheckUserNameExists/{userName}")]
        public async Task<IActionResult> CheckUserNameExists(string userName)
        {
            return new OkObjectResult(await _registerUserService.CheckUserExists(userName));
        }

        [HttpGet("CheckEmailAddress/{email}")]
        public async Task<IActionResult> CheckEmailAddress(string email)
        {
            return new OkObjectResult(await _registerUserService.CheckEmailAddress(email));
        }

        [HttpPost("ForgotUserName")]
        public async Task<IActionResult> Post([FromBody] ForgotUserNameViewModel model)
        {
            return new OkObjectResult(await _registerUserService.ForgotUserName(model.Email));
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> Post([FromBody] ForgotUserNameChangePasswordViewModel model)
        {
            return new OkObjectResult(await _registerUserService.ChangePassword(model));
        }

        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserViewModel model)
        {
            return new OkObjectResult(_registerUserService.CreateUser(model));
        }

        [HttpGet("GetCountries")]
        public IActionResult GetCountries()
        {
            return new OkObjectResult(_registerUserService.GetCountries());
        }
    }
}