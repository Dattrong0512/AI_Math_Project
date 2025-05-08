using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class PlanTransaction
{
    public int PlanTransactionId { get; set; }

    public int? UserId { get; set; }

    public int? PlanId { get; set; }

    public int? PaymentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Payment? Payment { get; set; }

    public virtual Plan? Plan { get; set; }

    public virtual User? User { get; set; }
}
