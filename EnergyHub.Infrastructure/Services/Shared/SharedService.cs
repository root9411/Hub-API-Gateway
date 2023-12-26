using EnergyHub.Application.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHub.Infrastructure.Services.Shared
{
    public class SharedService : ISharedService
    {
        const string sharedSecret = "FE835FC7-7EAA-4734-AE97-11BCD83A970C";

        public string EncryptText(string plainText)
        {
            return SecurityOperation.EncryptStringAES(plainText, sharedSecret);
        }

        public string DecryptText(string encryptedText)
        {
            return SecurityOperation.DecryptStringAES(encryptedText, sharedSecret);
        }
    }
}
