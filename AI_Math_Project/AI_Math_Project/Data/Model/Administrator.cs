using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Index("Email", Name = "UQ__Administ__AB6E6164A30023D3", IsUnique = true)]
public partial class Administrator
{
    [Key]
    [Column("admin_id")]
    public int AdminId { get; set; }

    [Column("admin_name")]
    [StringLength(100)]
    public string AdminName { get; set; } = null!;

    [Column("email")]
    [StringLength(255)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("admin_password")]
    [StringLength(255)]
    [Unicode(false)]
    public string AdminPassword { get; set; } = null!;

    [Column("gender")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Gender { get; set; }

    [Column("dob")]
    public DateOnly? Dob { get; set; }

    [Column("avatar", TypeName = "text")]
    public string? Avatar { get; set; }

    [Column("status")]
    public bool? Status { get; set; }
}
