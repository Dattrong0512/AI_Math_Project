using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class ExerciseResult
{
    public int ExerciseResultId { get; set; }

    public int? ExerciseId { get; set; }

    public int? EnrollmentId { get; set; }

    public int? CompletionTime { get; set; }

    public decimal? Score { get; set; }

    public DateTime? DoneAt { get; set; }

    public virtual Enrollment? Enrollment { get; set; }

    public virtual Exercise? Exercise { get; set; }

    public virtual ICollection<ExerciseDetailResult> ExerciseDetailResults { get; set; } = new List<ExerciseDetailResult>();
}
