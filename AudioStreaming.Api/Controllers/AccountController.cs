using ASS_PRC.Api.Models.Request;
using ASS_PRC.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASS_PRC.Api.Controllers
{
    [Route(Helpers.SettingVersionAPI.ApiVersion)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate-webadmin")]
        public async Task<IActionResult> AuthenticateWebAdminAsync([FromBody] AuthenticateModelWebAdmin model)
        {
            var authenticateResponse = await _accountService.AuthenticateWebAdminAsync(model.IdToken);

            if (authenticateResponse == null)
                return Unauthorized();
            return Ok(authenticateResponse);
        }       
    }
}