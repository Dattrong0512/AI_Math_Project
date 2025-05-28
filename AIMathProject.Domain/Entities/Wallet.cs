using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Wallet
{
    public int WalletId { get; set; }

    public int UserId { get; set; }

    public int CoinRemains { get; set; }

    public int TokenRemains { get; set; }

    public virtual ICollection<CoinTransaction> CoinTransactions { get; set; } = new List<CoinTransaction>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<TokenTransaction> TokenTransactions { get; set; } = new List<TokenTransaction>();

    public virtual User User { get; set; } = null!;
}
