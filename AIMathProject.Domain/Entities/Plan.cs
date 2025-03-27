using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Plan
{
    public int PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public decimal? Price { get; set; }

    public int? DurationDays { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<PlanTransaction> PlanTransactions { get; set; } = new List<PlanTransaction>();
}
