using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Model;

public partial class Notification
{
    [Key]
    [Column("notification_id")]
    public int NotificationId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("notification_type")]
    [StringLength(50)]
    [Unicode(false)]
    public string? NotificationType { get; set; }

    [Column("notification_title")]
    [StringLength(255)]
    [Unicode(false)]
    public string? NotificationTitle { get; set; }

    [Column("notification_message")]
    [StringLength(255)]
    public string? NotificationMessage { get; set; }

    [Column("sent_at", TypeName = "datetime")]
    public DateTime? SentAt { get; set; }

    [Column("status")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Status { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Notifications")]
    public virtual User? User { get; set; }
}
