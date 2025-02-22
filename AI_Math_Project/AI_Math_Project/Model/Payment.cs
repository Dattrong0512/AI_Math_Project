using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Model;

[Table("Payment")]
public partial class Payment
{
    [Key]
    [Column("payment_id")]
    public int PaymentId { get; set; }

    [Column("payment_name")]
    [StringLength(100)]
    public string PaymentName { get; set; } = null!;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("status")]
    public bool? Status { get; set; }

    [InverseProperty("Payment")]
    public virtual ICollection<PlanTransaction> PlanTransactions { get; set; } = new List<PlanTransaction>();

    [InverseProperty("Payment")]
    public virtual ICollection<TokenTransaction> TokenTransactions { get; set; } = new List<TokenTransaction>();
}
