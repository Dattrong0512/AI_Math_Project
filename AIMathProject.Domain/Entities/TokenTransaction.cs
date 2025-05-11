using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class TokenTransaction
{
    public int TokenTransactionId { get; set; }

    public int? TokenUserId { get; set; }

    public int? Amount { get; set; }

    public DateTime? Date { get; set; }

    public virtual TokenUser? TokenUser { get; set; }
}
