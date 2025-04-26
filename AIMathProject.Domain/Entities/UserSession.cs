using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Entities
{
    public class UserSession
    {
        public int UserSessionId { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public TimeSpan? Duration { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }
}
