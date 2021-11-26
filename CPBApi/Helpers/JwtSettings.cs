using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPBApi.Helpers
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int Expiration { get; set; }
        public string Issuer { get; set; }
    }
}
