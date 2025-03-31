using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Requests
{
    public record UpdateRequest
    {
        public string? UserName { get; init; }
        public bool? Gender { get; init; }
        public DateTime Dob { get; init; }
        public string? Avatar { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
