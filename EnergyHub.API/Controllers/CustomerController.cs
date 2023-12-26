using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.AspNetCore.Authorization;
using EnergyHub.Application.Queries.Customer;
using EnergyHub.Application.Commands.Customer;
using System.IdentityModel.Tokens.Jwt;
using EnergyHub.Application.Common;
using EnergyHub.Domain.Models.Token;

namespace EnergyHub.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var httpRequest = HttpContext.Request;
            string authorizationHeader = httpRequest.Headers["Authorization"];

            AuthorizationHeader header = new AuthorizationHeader();
            TokenPayload headRes = header.GetTokenPayloadData(authorizationHeader);
            
            GetCustomerRequest req = new GetCustomerRequest();
            req.Id = headRes.Id;
            req.EnvironmentDbName = headRes.EnvironmentDatabaseName;

            var response = await _mediator.Send(req);
            return Ok(response);
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create(CreateCustomerRequest payload)
        {
            var newlyCreateItemId = await _mediator.Send(payload);
            return Ok(newlyCreateItemId);
        }
    }
}
