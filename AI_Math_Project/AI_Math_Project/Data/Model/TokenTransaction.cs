using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Token_Transaction")]
public partial class TokenTransaction
{
    [Key]
    [Column("token_transaction_id")]
    public int TokenTransactionId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("token_package_id")]
    public int? TokenPackageId { get; set; }

    [Column("payment_id")]
    public int? PaymentId { get; set; }

    [Column("change")]
    public int? Change { get; set; }

    [Column("transaction_type")]
    [StringLength(5)]
    [Unicode(false)]
    public string? TransactionType { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("PaymentId")]
    [InverseProperty("TokenTransactions")]
    public virtual Payment? Payment { get; set; }

    [ForeignKey("TokenPackageId")]
    [InverseProperty("TokenTransactions")]
    public virtual TokenPackage? TokenPackage { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TokenTransactions")]
    public virtual User? User { get; set; }
}
