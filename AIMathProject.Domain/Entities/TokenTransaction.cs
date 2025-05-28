using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class TokenTransaction
{
    public int TokenTransactionId { get; set; }

    public int WalletId { get; set; }

    public int? TokenRemains { get; set; }

    public int? TokenAmount { get; set; }

    public DateTime? Date { get; set; }

    public virtual Wallet Wallet { get; set; } = null!;
}
