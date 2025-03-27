using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Test
{
    public int TestId { get; set; }

    public int? ChapterId { get; set; }

    public string? TestName { get; set; }

    public virtual Chapter? Chapter { get; set; }

    public virtual ICollection<TestDetail> TestDetails { get; set; } = new List<TestDetail>();

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}
