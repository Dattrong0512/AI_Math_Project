using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Requests
{
    public record RegisterRequest
    {
        public required string UserName { get; init; }
        public required string Email { get; init; }
        public required bool Gender { get; init; }
        public required DateTime Dob { get; init; }
        public required string Avatar { get; init; }
        public required string Password { get; init; }
        public required bool Status { get; init; }

    }
}
