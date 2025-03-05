using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Exercise_Detail_Result")]
public partial class ExerciseDetailResult
{
    [Key]
    [Column("exercise_detail_result_id")]
    public int ExerciseDetailResultId { get; set; }

    [Column("exercise_detail_id")]
    public int? ExerciseDetailId { get; set; }

    [Column("exercise_result_id")]
    public int? ExerciseResultId { get; set; }

    [Column("is_correct")]
    public bool? IsCorrect { get; set; }

    [ForeignKey("ExerciseDetailId")]
    [InverseProperty("ExerciseDetailResults")]
    public virtual ExerciseDetail? ExerciseDetail { get; set; }

    [ForeignKey("ExerciseResultId")]
    [InverseProperty("ExerciseDetailResults")]
    public virtual ExerciseResult? ExerciseResult { get; set; }
}
