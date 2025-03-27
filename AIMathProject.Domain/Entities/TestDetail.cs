using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class TestDetail
{
    public int TestDetailId { get; set; }

    public int? TestId { get; set; }

    public int? QuestionId { get; set; }

    public virtual Question? Question { get; set; }

    public virtual Test? Test { get; set; }

    public virtual ICollection<TestDetailResult> TestDetailResults { get; set; } = new List<TestDetailResult>();
}
