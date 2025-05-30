﻿using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Plan
{
    public int PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public decimal? Price { get; set; }

    public int? Coins { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
