using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Account")]
[Index("Email", Name = "UQ__Account__AB6E61643D0F43A2", IsUnique = true)]
public partial class Account
{
    [Key]
    [Column("account_id")]
    public int AccountId { get; set; }

    [Column("email")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("password")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Password { get; set; }

    [Column("description")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("role")]
    public int? Role { get; set; }

    [InverseProperty("Admin")]
    public virtual Administrator? Administrator { get; set; }

    [InverseProperty("Account")]
    public virtual RefreshToken? RefreshToken { get; set; }

    [InverseProperty("UserNavigation")]
    public virtual User? User { get; set; }
}
