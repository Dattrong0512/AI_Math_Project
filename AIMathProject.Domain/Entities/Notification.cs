using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public string? NotificationType { get; set; }

    public string? NotificationTitle { get; set; }

    public string? NotificationMessage { get; set; }

    public DateTime? SentAt { get; set; }

    public string? Status { get; set; }

    public virtual User? User { get; set; }
}
