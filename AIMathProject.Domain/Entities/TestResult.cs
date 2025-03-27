using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class TestResult
{
    public int TestResultId { get; set; }

    public int? TestId { get; set; }

    public int? EnrollmentId { get; set; }

    public decimal? Score { get; set; }

    public short? CompletionTime { get; set; }

    public DateTime? DoneAt { get; set; }

    public virtual Enrollment? Enrollment { get; set; }

    public virtual Test? Test { get; set; }

    public virtual ICollection<TestDetailResult> TestDetailResults { get; set; } = new List<TestDetailResult>();
}
