using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Model;

[Table("Lesson")]
public partial class Lesson
{
    [Key]
    [Column("lesson_id")]
    public int LessonId { get; set; }

    [Column("lesson_order")]
    public short? LessonOrder { get; set; }

    [Column("lesson_name")]
    [StringLength(100)]
    public string LessonName { get; set; } = null!;

    [Column("chapter_id")]
    public int? ChapterId { get; set; }

    [Column("lesson_content")]
    [StringLength(255)]
    public string? LessonContent { get; set; }

    [ForeignKey("ChapterId")]
    [InverseProperty("Lessons")]
    public virtual Chapter? Chapter { get; set; }

    [InverseProperty("Lesson")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("Lesson")]
    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    [InverseProperty("Lesson")]
    public virtual ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();

    [InverseProperty("Lesson")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
