using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

public partial class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("user_name")]
    [StringLength(100)]
    public string UserName { get; set; } = null!;

    [Column("gender")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Gender { get; set; }

    [Column("balance")]
    public int? Balance { get; set; }

    [Column("is_premium")]
    public bool? IsPremium { get; set; }

    [Column("dob")]
    public DateOnly? Dob { get; set; }

    [Column("avatar")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Avatar { get; set; }

    [Column("status")]
    public bool? Status { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    [InverseProperty("User")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("User")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    [InverseProperty("User")]
    public virtual ICollection<ErrorReport> ErrorReports { get; set; } = new List<ErrorReport>();

    [InverseProperty("User")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("User")]
    public virtual ICollection<PlanTransaction> PlanTransactions { get; set; } = new List<PlanTransaction>();

    [InverseProperty("User")]
    public virtual ICollection<TokenTransaction> TokenTransactions { get; set; } = new List<TokenTransaction>();

    [ForeignKey("UserId")]
    [InverseProperty("User")]
    public virtual Account UserNavigation { get; set; } = null!;
}
