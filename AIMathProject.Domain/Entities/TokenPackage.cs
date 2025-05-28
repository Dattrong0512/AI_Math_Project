using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class TokenPackage
{
    public int TokenPackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public int? Tokens { get; set; }

    public int? Price { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<CoinTransaction> CoinTransactions { get; set; } = new List<CoinTransaction>();
}
