using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class EnrollmentUnlockExercise
{
    public int EnrollmentUnlockExerciseId { get; set; }

    public int? ExerciseId { get; set; }

    public int? EnrollmentId { get; set; }

    public DateTime? UnlockDate { get; set; }

    public virtual Exercise? Exercise { get; set; }

    public virtual Enrollment? Enrollment { get; set; }
}
