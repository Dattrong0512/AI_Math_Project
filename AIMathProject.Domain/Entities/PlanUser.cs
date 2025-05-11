using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class PlanUser
{
    public int PlanUserId { get; set; }

    public int? UserId { get; set; }

    public int? Coins { get; set; }

    public virtual ICollection<PlanTransaction> PlanTransactions { get; set; } = new List<PlanTransaction>();

    public virtual User? User { get; set; }
}
