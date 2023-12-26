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
        private readonly IAntiforgery _antiforgery;

        public AuthenticationController(IAuthService authService, IHttpContextAccessor context,  IAntiforgery antiforgery)
        {
            _authService = authService;
            _context = context;
            _antiforgery = antiforgery;
        }

        //[HttpGet]
        //[ActionName("SignIn")]
        //public async Task<IActionResult> SignIn()
        //{
        //    var response = await _authService.GetAccessTokenAsync();
        //    return Ok(response);
        //}

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginDetail user)
        {
            try
            {
                var result = await _authService.GetAccessToken(user);
                //var httpcon = _context.HttpContext;

                //httpcon.Response.Cookies.Append(user.ClientId, result.RefreshToken, new CookieOptions
                //{
                //    Expires = DateTime.Now.AddHours(1),
                //    Secure = true, 
                //    HttpOnly = true, 
                //    SameSite = SameSiteMode.Strict 
                //});

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
            //var httpcon = _context.HttpContext;

            var result = await _authService.GetPrincipleFromExpireToken(refresh);

            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }


        
    }
}
