using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHub.Application.Services.Shared
{
    public interface ISharedService
    {
        public string EncryptText(string plainText);
        public string DecryptText(string encryptedText);
    }
}
