using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string PaymentName { get; set; } = null!;

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<PlanTransaction> PlanTransactions { get; set; } = new List<PlanTransaction>();

    public virtual ICollection<TokenTransaction> TokenTransactions { get; set; } = new List<TokenTransaction>();
}
