using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Model;

[Table("Lesson_Progress")]
public partial class LessonProgress
{
    [Key]
    [Column("learning_progress_id")]
    public int LearningProgressId { get; set; }

    [Column("lesson_id")]
    public int? LessonId { get; set; }

    [Column("enrollment_id")]
    public int? EnrollmentId { get; set; }

    [Column("learning_progress")]
    public short? LearningProgress { get; set; }

    [Column("is_completed")]
    public bool? IsCompleted { get; set; }

    [ForeignKey("EnrollmentId")]
    [InverseProperty("LessonProgresses")]
    public virtual Enrollment? Enrollment { get; set; }

    [ForeignKey("LessonId")]
    [InverseProperty("LessonProgresses")]
    public virtual Lesson? Lesson { get; set; }
}
