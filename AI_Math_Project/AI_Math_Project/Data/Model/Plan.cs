using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

public partial class Plan
{
    [Key]
    [Column("plan_id")]
    public int PlanId { get; set; }

    [Column("plan_name")]
    [StringLength(100)]
    public string PlanName { get; set; } = null!;

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [Column("duration_days")]
    public int? DurationDays { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [InverseProperty("Plan")]
    public virtual ICollection<PlanTransaction> PlanTransactions { get; set; } = new List<PlanTransaction>();
}
