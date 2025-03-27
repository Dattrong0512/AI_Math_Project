using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Exercise
{
    public int ExerciseId { get; set; }

    public string ExerciseName { get; set; } = null!;

    public int? LessonId { get; set; }

    public virtual ICollection<ExerciseDetail> ExerciseDetails { get; set; } = new List<ExerciseDetail>();

    public virtual ICollection<ExerciseResult> ExerciseResults { get; set; } = new List<ExerciseResult>();

    public virtual Lesson? Lesson { get; set; }
}
