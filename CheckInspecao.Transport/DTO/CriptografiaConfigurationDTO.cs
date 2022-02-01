using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInspecao.Transport.DTO
{
    public class CriptografiaConfigurationDTO
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }
    }
}
