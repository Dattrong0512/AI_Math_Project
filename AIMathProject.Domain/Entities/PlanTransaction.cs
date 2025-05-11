using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class PlanTransaction
{
    public int PlanTransactionId { get; set; }

    public int? PlanUserId { get; set; }

    public int? Amount { get; set; }

    public DateTime? Date { get; set; }

    public virtual PlanUser? PlanUser { get; set; }
}
