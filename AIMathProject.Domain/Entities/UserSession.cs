using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class UserSession
{
    public int UserSessionId { get; set; }

    public int UserId { get; set; }

    public DateTime LoginTime { get; set; }

    public DateTime? LogoutTime { get; set; }

    public TimeOnly? Duration { get; set; }

    public virtual User User { get; set; } = null!;
}
