using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Model;

[Table("Enrollment")]
public partial class Enrollment
{
    [Key]
    [Column("enrollment_id")]
    public int EnrollmentId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("grade")]
    public short? Grade { get; set; }

    [Column("enrollment_date")]
    public DateOnly? EnrollmentDate { get; set; }

    [Column("avg_score", TypeName = "decimal(2, 2)")]
    public decimal? AvgScore { get; set; }

    [Column("semester")]
    public short? Semester { get; set; }

    [Column("start_year")]
    public short? StartYear { get; set; }

    [Column("end_year")]
    public short? EndYear { get; set; }

    [InverseProperty("Enrollment")]
    public virtual ICollection<ExerciseResult> ExerciseResults { get; set; } = new List<ExerciseResult>();

    [InverseProperty("Enrollment")]
    public virtual ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();

    [InverseProperty("Enrollment")]
    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();

    [ForeignKey("UserId")]
    [InverseProperty("Enrollments")]
    public virtual User? User { get; set; }
}
