using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int? UserId { get; set; }

    public short? Grade { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    public decimal? AvgScore { get; set; }

    public short? Semester { get; set; }

    public short? StartYear { get; set; }

    public short? EndYear { get; set; }

    public virtual ICollection<ExerciseResult> ExerciseResults { get; set; } = new List<ExerciseResult>();

    public virtual ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();

    public virtual User? User { get; set; }
}
