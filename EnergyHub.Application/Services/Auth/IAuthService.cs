using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using EnergyHub.Domain.Models.Login;
using EnergyHub.Domain.Models.Token;
using System.Security.Claims;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace EnergyHub.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthenticationResponse> GetAccessToken(Guid ClientId, Claim[] claims);
        Task<AuthenticationResponse> GetAccessToken(LoginDetail user);
        string RefreshTokenGenerator();
        Task<AuthenticationResponse> GetPrincipleFromExpireToken(AuthenticationResponse response);
    }
}
