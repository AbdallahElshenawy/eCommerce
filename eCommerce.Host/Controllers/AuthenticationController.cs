using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.Services.Implementaions.Authentication;
using eCommerce.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUser user)
        {
            var result = await authenticationService.CreateUser(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginUser user)
        {
            var result = await authenticationService.LoginUser(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("refreshToken/{refreshToken}")]
        public async Task<IActionResult> ReviveToken(string refreshToken)
        {
            var result = await authenticationService.ReviveToken(refreshToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
