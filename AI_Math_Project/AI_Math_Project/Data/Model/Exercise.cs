using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Exercise")]
public partial class Exercise
{
    [Key]
    [Column("exercise_id")]
    public int ExerciseId { get; set; }

    [Column("exercise_name")]
    [StringLength(100)]
    public string ExerciseName { get; set; } = null!;

    [Column("lesson_id")]
    public int? LessonId { get; set; }

    [Column("exercise_score", TypeName = "decimal(2, 2)")]
    public decimal? ExerciseScore { get; set; }

    [InverseProperty("Exercise")]
    public virtual ICollection<ExerciseDetail> ExerciseDetails { get; set; } = new List<ExerciseDetail>();

    [InverseProperty("Exercise")]
    public virtual ICollection<ExerciseResult> ExerciseResults { get; set; } = new List<ExerciseResult>();

    [ForeignKey("LessonId")]
    [InverseProperty("Exercises")]
    public virtual Lesson? Lesson { get; set; }
}
