using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Options
{
    public class JwtOptions
    {
        public const string JwtOptionKey = "JwtOptions";
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationTimeInMinutes { get; set; }
    }
}
