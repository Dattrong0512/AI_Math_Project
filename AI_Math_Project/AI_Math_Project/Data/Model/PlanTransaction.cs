using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Plan_Transaction")]
public partial class PlanTransaction
{
    [Key]
    [Column("plan_transaction_id")]
    public int PlanTransactionId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("plan_id")]
    public int? PlanId { get; set; }

    [Column("payment_id")]
    public int? PaymentId { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("expires_at", TypeName = "datetime")]
    public DateTime? ExpiresAt { get; set; }

    [ForeignKey("PaymentId")]
    [InverseProperty("PlanTransactions")]
    public virtual Payment? Payment { get; set; }

    [ForeignKey("PlanId")]
    [InverseProperty("PlanTransactions")]
    public virtual Plan? Plan { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("PlanTransactions")]
    public virtual User? User { get; set; }
}
