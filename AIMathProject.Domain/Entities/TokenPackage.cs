using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class TokenPackage
{
    public int TokenPackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public int? Tokens { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<TokenTransaction> TokenTransactions { get; set; } = new List<TokenTransaction>();
}
