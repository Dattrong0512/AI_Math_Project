using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("RefreshToken")]
public partial class RefreshToken
{
    [Key]
    [Column("account_id")]
    public int AccountId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Token { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiryDate { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("RefreshToken")]
    public virtual Account Account { get; set; } = null!;
}
