using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Model;

[Table("Exercise_Result")]
public partial class ExerciseResult
{
    [Key]
    [Column("exercise_result_id")]
    public int ExerciseResultId { get; set; }

    [Column("exercise_id")]
    public int? ExerciseId { get; set; }

    [Column("enrollment_id")]
    public int? EnrollmentId { get; set; }

    [Column("score", TypeName = "decimal(2, 2)")]
    public decimal? Score { get; set; }

    [Column("done_at", TypeName = "datetime")]
    public DateTime? DoneAt { get; set; }

    [ForeignKey("EnrollmentId")]
    [InverseProperty("ExerciseResults")]
    public virtual Enrollment? Enrollment { get; set; }

    [ForeignKey("ExerciseId")]
    [InverseProperty("ExerciseResults")]
    public virtual Exercise? Exercise { get; set; }
}
