using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.LoginDto
{
    public class RefreshTokenResponseDto
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
