using System.Threading.Tasks;
using Dms.Services.ViewModel.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneStopApp_Api.ViewModel;

namespace Dms.Api.Designee.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class RegisterDesigneeController
    {
        private readonly IRegisterUserService _registerDesigneeService;
        private readonly ILogger _logger;
        public RegisterDesigneeController(ILogger<RegisterDesigneeController> logger, IRegisterUserService registerDesigneeServices)
        {
            _registerDesigneeService = registerDesigneeServices; 
            _logger = logger;          
        }

        [HttpGet("CheckUserNameExists/{userName}")]       
        public async Task<IActionResult> CheckUserNameExists(string userName)
        {
            return new OkObjectResult(await _registerDesigneeService.CheckUserExists(userName));
        }

        [HttpGet("CheckEmailAddress/{email}")]       
        public async Task<IActionResult> CheckEmailAddress(string email)
        {
            return new OkObjectResult(await _registerDesigneeService.CheckEmailAddress(email));
        }
        
        [HttpPost("ForgotUserName")]
        public async Task<IActionResult> Post([FromBody]ForgotUserNameViewModel model)
        {
            return new OkObjectResult(await _registerDesigneeService.ForgotUserName(model.Email));
        }        
               
        [HttpPost("ValidateUserNameAndEmail")]
        public async Task<IActionResult> Post([FromBody]ForgotPasswordViewModel model)
        {
            return new OkObjectResult(await _registerDesigneeService.ValidateUserNameAndEmail(model.UserName, model.Email));
        }

        
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> Post([FromBody]ForgotUserNameChangePasswordViewModel model)
        {
            return new OkObjectResult(await _registerDesigneeService.ChangePassword(model));
        }

        [HttpPost]
        public IActionResult Post([FromBody]RegisterUserViewModel model)
        {
            return new OkObjectResult(_registerDesigneeService.CreateDesignee(model));            
        }
    }
}