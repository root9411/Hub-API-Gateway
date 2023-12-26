using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHub.Domain.Models.Token
{
    public class AuthenticationResponse
    {
        public string accessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime accessTokenExpirationTime { get; set; }
    }
}
