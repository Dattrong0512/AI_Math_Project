using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class ExerciseDetail
{
    public int ExerciseDetailId { get; set; }

    public int? ExerciseId { get; set; }

    public int? QuestionId { get; set; }

    public virtual Exercise? Exercise { get; set; }

    public virtual ICollection<ExerciseDetailResult> ExerciseDetailResults { get; set; } = new List<ExerciseDetailResult>();

    public virtual Question? Question { get; set; }
}
