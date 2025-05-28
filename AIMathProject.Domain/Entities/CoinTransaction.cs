using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class CoinTransaction
{
  
    public int CoinTransactionId { get; set; }

    public int WalletId { get; set; }

    public int? TokenPackageId { get; set; }

    public bool? IsTokenPackage { get; set; }

    public int? CoinRemains { get; set; }

    public int? Amount { get; set; }

    public DateTime? Date { get; set; }

    public virtual TokenPackage? TokenPackage { get; set; }

    public virtual Wallet Wallet { get; set; } = null!;
}
