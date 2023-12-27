using System.Text;
using System.Security.Claims;
using EnergyHub.Domain.Models.Login;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using EnergyHub.Application.Services.Auth;
using EnergyHub.Application.Services.Shared;
using EnergyHub.Application.Services.Customer;
using System.Security.Cryptography;
using EnergyHub.Domain.Models.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EnergyHub.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomerService _customerService;
        private readonly ISharedService _sharedService;
     

       

        public AuthService(IConfiguration configuration, ICustomerService customerService, ISharedService sharedService)
        {
            _configuration = configuration;
            _customerService = customerService;
            _sharedService = sharedService;
           
        }

        public async Task<AuthenticationResponse> GetAccessToken(Guid Id,Claim[] claims)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var accessTokenExpirationTime = DateTime.UtcNow.AddMinutes(5);
            var jwtSecurityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: accessTokenExpirationTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key), 
                        SecurityAlgorithms.HmacSha512)
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = RefreshTokenGenerator();

            AuthenticationResponse TokenInfo = new AuthenticationResponse()
            {
                accessToken = jwtToken,
                RefreshToken = refreshToken,
            };

            var FirstToken = false;


            var result = await _customerService.UpdateTokenInfo(TokenInfo, Id, FirstToken);

            if (result)
            {
                return TokenInfo;
            }

            throw new SecurityTokenException("Token Not Updated!");
        }



        public async Task<AuthenticationResponse> GetAccessToken(LoginDetail user)
        {
            var userDetail = await _customerService.GetCustomerListAsync();

            if (userDetail != null)
            {
                if (user != null)
                {
                    foreach (var item in userDetail)
                    {
                        string decryptedPassword = _sharedService.DecryptText(item.ClientSecret);

                        if (user.ClientId.ToUpper() == item.ClientId.ToString().ToUpper() && user.ClientSecret.Equals(decryptedPassword))
                        {
                            var issuer = _configuration["Jwt:Issuer"];
                            var audience = _configuration["Jwt:Audience"];
                            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512);

                            var subject = new ClaimsIdentity(new[]
                            {
                                new Claim("Id", item.Id.ToString()),
                                new Claim("DatabaseName", item.EnvironmentDatabaseName.ToString()),
                                //new Claim("ClientId", item.ClientId.ToString()),
                                new Claim(ClaimTypes.Name, item.Id.ToString()),

                                //new Claim(JwtRegisteredClaimNames.Sub, user.ClientId.ToString()),
                                //new Claim(JwtRegisteredClaimNames.Email, user.ClientId.ToString()),
                            });

                            var accessTokenExpirationTime = DateTime.UtcNow.AddMinutes(5);

                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = subject,
                                Expires = accessTokenExpirationTime,
                                Issuer = issuer,
                                Audience = audience,
                                SigningCredentials = signingCredentials
                            };

                            var tokenHandler = new JwtSecurityTokenHandler();
                            var token = tokenHandler.CreateToken(tokenDescriptor);
                            var jwtToken = tokenHandler.WriteToken(token);

                            var refreshToken = RefreshTokenGenerator();
                            var refreshTokenExpire = DateTime.UtcNow.AddDays(9);  // refresh Token Expire


                            AuthenticationResponse TokenInfo = new AuthenticationResponse() { 
                                accessToken = jwtToken,
                                RefreshToken = refreshToken,
                                accessTokenExpirationTime = refreshTokenExpire,
                            };

                            var FirstToken = true;


                            var result = await _customerService.UpdateTokenInfo(TokenInfo, item.Id,FirstToken);
                            if (result)
                            {
                                return TokenInfo;
                            }
                            
                            throw new SecurityTokenException("Token Not Updated!");

                        }
                    }
                    return new AuthenticationResponse
                    {
                        accessToken = "Please Enter Valid Credentials",
                        RefreshToken = "Please Enter Valid Credentials",
                    };
                }
            }
            return null;


            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Accept.Clear();

            //    var requestBody = new Dictionary<string, string>
            //    {
            //        { "grant_type", "authorization_code" },
            //        { "client_id", "f41f4765-3d15-466e-9ad0-336cd21421ef" },
            //        { "client_secret", "uD59g_r-LiT208C~OTAasw.t3Er.gs6aZg" },
            //        { "scope", "openid profile your-api-scope" },
            //        { "code", "authorization_code" },
            //        { "redirect_uri", "https://localhost:44376/" }
            //    };

            //    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


            //    var response = httpClient.PostAsync("https://utilidexalpha.b2clogin.com/utilidexalpha.onmicrosoft.com/B2C_1_susi/oauth2/v2.0/token", new FormUrlEncodedContent(requestBody)).Result;

            //    string contents = response.Content.ReadAsStringAsync().Result;

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var tokenResponse = await response.Content.ReadAsStringAsync();
            //        return "";
            //    }
            //    else
            //    {
            //        // Handle error
            //        Console.WriteLine($"Token request failed: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            //        return null;
            //    }
            //}

            
        }

        public string RefreshTokenGenerator()
        {
            var randomNumber = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<AuthenticationResponse> GetPrincipleFromExpireToken(AuthenticationResponse response)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            SecurityToken validatedToken;
            var pricipal = tokenHandler.ValidateToken(response.accessToken,
                new TokenValidationParameters
                {
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                },out validatedToken);


            var jwtToken = validatedToken as JwtSecurityToken;
            if(jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token Passed!");
            }


            var Id = pricipal.Identity.Name;
            var accessTokenExpirationTime = DateTime.UtcNow.AddMinutes(5);


            if (Guid.TryParse(Id,out Guid ClientData))
            {
                var result = _customerService.GetCustomerAsync(ClientData);

                if (response.RefreshToken != result.Result.RefreshToken)
                {
                    throw new SecurityTokenException("Invalid token Passed!");
                }

                if(accessTokenExpirationTime > result.Result.accessTokenExpirationTime)
                {
                    throw new SecurityTokenException("Refresh Token Expired Login Again!");
                }

                return await GetAccessToken(ClientData, pricipal.Claims.ToArray());

            }
            

            throw new SecurityTokenException("Invalid Token Claims!");


        }
    }
}
