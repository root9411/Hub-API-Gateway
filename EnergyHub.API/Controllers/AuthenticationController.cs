using Microsoft.AspNetCore.Mvc;
using EnergyHub.Domain.Models.Login;
using Microsoft.AspNetCore.Authorization;
using EnergyHub.Application.Services.Auth;
using EnergyHub.Domain.Models.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Antiforgery;

namespace EnergyHub.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _context;

        public AuthenticationController(IAuthService authService, IHttpContextAccessor context)
        {
            _authService = authService;
            _context = context;
        }


        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginDetail user)
        {
            try
            {
                var result = await _authService.GetAccessToken(user);
                

                return Ok(result);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost("Refresh")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Refresh([FromBody] AuthenticationResponse refresh)
        {

            var result = await _authService.GetPrincipleFromExpireToken(refresh);

            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }


        
    }
}
