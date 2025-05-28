using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int MethodId { get; set; }

    public int WalletId { get; set; }

    public string? OrderId { get; set; }

    public string? TransactionId { get; set; }

    public int? PlanId { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public decimal? Price { get; set; }

    public virtual PaymentMethod Method { get; set; } = null!;

    public virtual Plan? Plan { get; set; }

    public virtual Wallet Wallet { get; set; } = null!;
}
