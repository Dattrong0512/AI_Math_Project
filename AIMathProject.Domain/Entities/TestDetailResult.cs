using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class TestDetailResult
{
    public int TestDetailResultId { get; set; }

    public int? TestDetailId { get; set; }

    public int? TestResultId { get; set; }

    public bool? IsCorrect { get; set; }

    public virtual TestDetail? TestDetail { get; set; }

    public virtual TestResult? TestResult { get; set; }
}
