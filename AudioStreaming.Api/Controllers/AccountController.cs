using AudioStreaming.Api.Models.Request;
using AudioStreaming.Services.Helpers;
using AudioStreaming.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AudioStreaming.Api.Controllers
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
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody]AuthenticateModel model)
        {
            var authenticateResponse = await _accountService.AuthenticateAsync(model.IdToken, model.fcmToken);

            if (authenticateResponse == null)
                return Unauthorized();
            return Ok(authenticateResponse);
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

        [HttpGet("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync()
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                string authHeader = Request.Headers["Authorization"];
                authHeader = authHeader.Replace("Bearer ", "");
                var jsonToken = handler.ReadJwtToken(authHeader);
                if (jsonToken.ValidTo < GetTimeNowVN.GetTimeNowVietNam())
                {
                    return Unauthorized();
                }

                var idString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var authenticateResponse =await _accountService.AuthenticateByIdAsync(new Guid(idString));
                return Ok(authenticateResponse);
            }  
            catch (Exception)
            {

                return Unauthorized();
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _accountService.GetAll();
            return Ok(users);
        }

    }
}