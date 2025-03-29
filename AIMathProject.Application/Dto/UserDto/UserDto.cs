using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.UserDto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? Gender { get; set; }
        public int? Balance { get; set; }
        public bool? IsPremium { get; set; }
        public DateTime? Dob { get; set; }
        public string? Avatar { get; set; }
        public bool? Status { get; set; }

    }
}
