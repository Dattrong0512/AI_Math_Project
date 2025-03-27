using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class TokenTransaction
{
    public int TokenTransactionId { get; set; }

    public int? UserId { get; set; }

    public int? TokenPackageId { get; set; }

    public int? PaymentId { get; set; }

    public int? Change { get; set; }

    public string? TransactionType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Payment? Payment { get; set; }

    public virtual TokenPackage? TokenPackage { get; set; }

    public virtual User? User { get; set; }
}
