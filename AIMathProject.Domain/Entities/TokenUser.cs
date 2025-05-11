using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class TokenUser
{
    public int TokenUserId { get; set; }

    public int? UserId { get; set; }

    public int? Tokens { get; set; }

    public virtual ICollection<TokenTransaction> TokenTransactions { get; set; } = new List<TokenTransaction>();

    public virtual User? User { get; set; }
}
