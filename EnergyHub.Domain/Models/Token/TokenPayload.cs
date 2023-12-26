using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHub.Domain.Models.Token
{
    public class TokenPayload
    {
        public string Id { get; set; }
        public string EnvironmentDatabaseName { get; set; }
    }
}
