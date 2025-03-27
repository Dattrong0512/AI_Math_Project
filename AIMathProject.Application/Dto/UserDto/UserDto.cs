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
        public string UserName { get; set; } = null!;
        public string? Gender { get; set; }
        public int? Balance { get; set; }
        public bool? IsPremium { get; set; }
        public DateOnly? Dob { get; set; }
        public string? Avatar { get; set; }
        public bool? Status { get; set; }

        public List<Chat> Chats { get; set; } = new List<Chat>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public List<ErrorReport> ErrorReports { get; set; } = new List<ErrorReport>();
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        public List<PlanTransaction> PlanTransactions { get; set; } = new List<PlanTransaction>();
        public List<TokenTransaction> TokenTransactions { get; set; } = new List<TokenTransaction>();

    }
}
