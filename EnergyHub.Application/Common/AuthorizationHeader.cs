using EnergyHub.Domain.Models.Token;
using System.IdentityModel.Tokens.Jwt;

namespace EnergyHub.Application.Common
{
    public class AuthorizationHeader
    {
        public TokenPayload GetTokenPayloadData(string authorizationHeader)
        {
            TokenPayload tokenPayload = new TokenPayload();

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                string token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                if (jsonToken != null)
                {
                    tokenPayload.Id = jsonToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
                    tokenPayload.EnvironmentDatabaseName = jsonToken.Claims.FirstOrDefault(c => c.Type == "DatabaseName")?.Value;
                }
            }
            return tokenPayload;
        }
    }
}
