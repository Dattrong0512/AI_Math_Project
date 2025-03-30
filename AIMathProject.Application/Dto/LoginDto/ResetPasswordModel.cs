using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto
{
    public class ResetPasswordModel
    {

        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
