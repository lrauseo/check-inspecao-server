using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInspecao.Transport.DTO
{
    public class JwtConfigurationDTO
    {

        public string Key { get; set; }
        public string Issuer { get; set; }
        public int DurationInMinutes { get; set; }
        public string Audience { get; set; }
    }
}
