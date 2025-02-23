using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Exercise_Detail")]
public partial class ExerciseDetail
{
    [Key]
    [Column("exercise_detail_id")]
    public int ExerciseDetailId { get; set; }

    [Column("exercise_id")]
    public int? ExerciseId { get; set; }

    [Column("question_id")]
    public int? QuestionId { get; set; }

    [ForeignKey("ExerciseId")]
    [InverseProperty("ExerciseDetails")]
    public virtual Exercise? Exercise { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("ExerciseDetails")]
    public virtual Question? Question { get; set; }
}
