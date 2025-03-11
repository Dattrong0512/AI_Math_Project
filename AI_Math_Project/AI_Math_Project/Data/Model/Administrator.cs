using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

public partial class Administrator
{
    [Key]
    [Column("admin_id")]
    public int AdminId { get; set; }

    [Column("admin_name")]
    [StringLength(100)]
    public string AdminName { get; set; } = null!;

    [Column("gender")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Gender { get; set; }

    [Column("dob")]
    public DateOnly? Dob { get; set; }

    [Column("avatar")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Avatar { get; set; }

    [Column("status")]
    public bool? Status { get; set; }

    [ForeignKey("AdminId")]
    [InverseProperty("Administrator")]
    public virtual Account Admin { get; set; } = null!;

    [InverseProperty("SupportAgent")]
    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
}
